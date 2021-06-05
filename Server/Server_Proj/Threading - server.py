############################################################################
# Basic Server that supports treads
############################################################################
import socket
import threading
import re
import SQL_Data, Server_Email
import time
class Server(object):

    def __init__(self, ip, port):
        self.ip = ip
        self.port = port
        self.username = ""
        self.username2 = ""
        self.player = 1
        self.ready = 0
        self.close = 0
        self.data = ""
        self.attacker = ""
        self.getting_attacked = ""
        self.gotdata = False
    def start(self):
        SQL_Data.Users()
        try:
           print('server starts up on ip %s port %s' % (self.ip, self.port))
           # Create a TCP/IP socket
           sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
           sock.bind((self.ip, self.port))
           sock.listen(2)
           self.handleClient(sock)

        except socket.error as e:
            print(e)

    def handleClient(self, sock):
        try:
            while True:
                if (self.player <= 2):
                    print('waiting for a new client')
                    clientSocket, client_address = sock.accept()  # block
                    print('new client entered')
                    player = self.player

                    self.player += 1
                    clientSocket.sendall("Connected to Server!".encode())
                    client_handler = threading.Thread(target=self.Register, args=(clientSocket,player))
                    # without comma you'd get a... TypeError: handle_client_connection() argument after * must be a sequence, not _socketobject
                    client_handler.start()



        except socket.error as e:
            print("hellooooo")
            print(e)

    def reset(self, player,username):
        self.ready -= 1
        self.close = 1
        print("reset")
        if (player == 1):
            self.player = 1
        if (player == 2):
            self.player = 2
        else:
            self.player = 1
        if (self.username == username):
            SQL_Data.Users().Update_user_characters(self.username, "0", "0", "0", "0", "0")
            SQL_Data.Users().Update_user_online(self.username, "0")
        elif (self.username2 == username):
            SQL_Data.Users().Update_user_characters(self.username2, "0", "0", "0", "0", "0")
            SQL_Data.Users().Update_user_online(self.username2, "0")
        print(self.player)

    def Register(self,client_socket,player):
        print(player)
        self.close = 0
        valid = True
        code = "0"
        code1 = "1"
        try:
            while (True):
                if(self.close == 1):
                    break
                user = client_socket.recv(1024).decode()
                print(user)
                if(user == "2"):
                    while (True):
                        Username = client_socket.recv(1024).decode()
                        print(Username)
                        if(Username == "              "):
                            print("YAY")
                            time.sleep(1)
                            break
                        if (Username == "exitexit32exitexit"):
                            self.close = 1
                            break
                        print("2")
                        Password = client_socket.recv(1024).decode()
                        print(Password)
                        Email = client_socket.recv(1024).decode()

                        print(Email + " " + Username + " " + Password)

                        if (self.Check_Email(Email)):
                            valid = False
                        print(valid)
                        if(SQL_Data.Users().select_user_by_User(Email, Username, Password,2)):
                            valid = False
                        print(valid)
                        if(not valid):
                            client_socket.sendall("Not valid".encode())
                            valid = True
                            print("fail")
                        else:
                            client_socket.sendall("valid".encode())
                            print("success")
                            while(code1 != code):
                                code = Server_Email.send_email(Email)
                                client_socket.sendall(str(code).encode())
                                code1 = client_socket.recv(1024).decode()
                                print(code1)
                                if(code1 == "exitexit32exitexit"):
                                    self.close = 1
                                    break
                                if(code1 == "              "):
                                    valid = True
                                    break
                                if(code1 == code):
                                    code = "0"
                                    code1 = "1"
                                    SQL_Data.Users().insert_user(Email, Username, Password)
                                    client_socket.sendall("You have successfully registered to the game!".encode())
                                    break
                            break

                elif (user == "1"):
                    self.Login(client_socket, player)
                    if (self.close == 1):
                        break
                elif(user != "1" and user !="2" and user != "exitexit32exitexit"):
                    print(user)
                    break
        except socket.error as e:
            print(e)
        if (player == 1):
            self.player = 1
            print(self.player)
        else:
            self.player = 2
            print(self.player)



    def Login(self, client_socket, player):
        try:
            while (True):
                Username = client_socket.recv(1024).decode()
                print(Username)
                if (Username == "              "):
                    print("YAY")
                    break
                if (Username == "exitexit32exitexit"):
                    self.close = 1
                    break
                Password = client_socket.recv(1024).decode()
                Email = client_socket.recv(1024).decode()

                if(SQL_Data.Users().select_user_by_User(Email,Username, Password,1)):
                    client_socket.sendall("You have successfully joined the game!".encode())
                    print("YAY")
                    if self.username == "":
                        self.username = Username
                    else:
                        self.username2 = Username
                    self.game(client_socket, Username, player)
                    break
                else:
                    client_socket.sendall("Not valid".encode())
        except socket.error as e:
            self.reset(player, Username)
            print(e)

    def Check_Email(self,Email):
        if (re.match(r"[^@]+@[^@]+\.[^@]+", Email)):
            return False
        return True


    def game(self,client_socket,username, player):
        try:
            win = SQL_Data.Users().getwin(username)
            lose = SQL_Data.Users().getlose(username)
            print(player)
            time.sleep(1)
            client_socket.sendall(win.encode())
            client_socket.sendall(lose.encode())
            SQL_Data.Users().Update_user_online(username, "True")
            c1 = client_socket.recv(1024).decode()
            print(str(c1))
            if(str(c1) == "              "):
                self.reset(player, username)
                self.Register(client_socket, player)
            if(str(c1) == "exitexit32exitexit"):
                self.reset(player, username)
            else:
                c2 = client_socket.recv(1024).decode()
                c3 = client_socket.recv(1024).decode()
                c4 = client_socket.recv(1024).decode()
                c5 = client_socket.recv(1024).decode()
                SQL_Data.Users().Update_user_characters(username, c1, c2, c3, c4, c5)
                print(player)
                client_socket.sendall(str(player).encode())
                self.ready += 1
                while(self.ready != 2):
                    time.sleep(1)
                    print(str(player) + ": " + str(self.ready))
                time.sleep(2)
                a = SQL_Data.Users().getcharacters(username)
                #if (not a):
                    #while (not a):
                        #time.sleep(1)
                        #a = SQL_Data.Users().getcharacters(username)
                        #if(a != []):
                            #break
                client_socket.sendall(str(a[0]).encode())
                time.sleep(0.5)
                client_socket.sendall(str(a[1]).encode())
                time.sleep(0.5)
                client_socket.sendall(str(a[2]).encode())
                time.sleep(0.5)
                client_socket.sendall(str(a[3]).encode())
                time.sleep(0.5)
                client_socket.sendall(str(a[4]).encode())
                if self.username == username:
                    time.sleep(0.5)
                    client_socket.sendall(self.username2.encode())
                else:
                    time.sleep(0.5)
                    client_socket.sendall(self.username.encode())

                while(True):
                    time.sleep(1)
                    for x in range(1, 27):
                        if (player == 1):
                            print(x)
                            self.data = client_socket.recv(1024).decode()
                            if (str(self.data) == "2"):
                                if (player == 1):
                                    self.attacker = client_socket.recv(1024).decode()
                                    self.getting_attacked = client_socket.recv(1024).decode()
                                    print(self.attacker + ", " + self.getting_attacked)
                                    self.gotdata = True
                            if(self.data == "exitexit32exitexit"):
                                SQL_Data.Users().Update_user_lose(username)
                                print(self.data)
                                self.gotdata = True
                                self.reset(player, username)
                                break
                            print(self.data)
                            self.gotdata = True

                        if (player == 2):
                            print("hello player 2")
                            while (self.gotdata != True):
                                time.sleep(0.001)
                            if(self.gotdata):
                                if (str(self.data) == "2"):
                                    if (player == 2):
                                        while (self.gotdata != True):
                                            time.sleep(0.1)
                                        if (self.gotdata):
                                            print(self.attacker + ", " + self.getting_attacked)
                                            time.sleep(1)
                                            client_socket.sendall(self.attacker.encode())
                                            time.sleep(1)
                                            client_socket.sendall(self.getting_attacked.encode())
                                            print("sent")
                                            self.attacker = ""
                                            self.getting_attacked = ""
                                    time.sleep(5)
                                    client_socket.sendall(self.data.encode())
                                    print("sent")
                                    print("next turn")
                                    self.data = ""
                                    self.gotdata = False
                                    break
                                else:
                                    client_socket.sendall(self.data.encode())
                                    print("sent")
                                if (str(self.data) == "exitexit32exitexit"):
                                    SQL_Data.Users().Update_user_characters(self.username, "0", "0", "0", "0", "0")
                                    SQL_Data.Users().Update_user_win(username)
                                    self.data = ""
                                    self.gotdata = False
                                    time.sleep(3)
                                    self.game(client_socket, username, player)
                                self.data = ""
                                self.gotdata = False
                        time.sleep(1)
                        if(player == 1 and x%5 == 0 and str(self.data) != "2" ):
                            self.attacker = client_socket.recv(1024).decode()
                            self.getting_attacked = client_socket.recv(1024).decode()
                            print(self.attacker + ", " + self.getting_attacked)
                            self.gotdata = True

                        if(player == 2 and x%5 == 0 and str(self.data) != "2"):
                            while (self.gotdata != True):
                                time.sleep(0.1)
                            if (self.gotdata):
                                print(self.attacker + ", " + self.getting_attacked)
                                time.sleep(1)
                                client_socket.sendall(self.attacker.encode())
                                time.sleep(1)
                                client_socket.sendall(self.getting_attacked.encode())
                                print("sent")
                                self.attacker = ""
                                self.getting_attacked = ""
                    time.sleep(1)

                    for x in range(1, 27):
                        if (player == 2):
                            print(x)
                            self.data = client_socket.recv(1024).decode()
                            if (self.data == "exitexit32exitexit"):
                                SQL_Data.Users().Update_user_lose(username)
                                print(self.data)
                                self.gotdata = True
                                self.reset(player, username)
                                break
                            print(self.data)
                            self.gotdata = True
                        if (player == 1):
                            print("hello player 1")
                            while (self.gotdata != True):
                                time.sleep(0.001)
                            if (self.gotdata):
                                if (str(self.data) == "2"):
                                    if (player == 2):
                                        self.attacker = client_socket.recv(1024).decode()
                                        self.getting_attacked = client_socket.recv(1024).decode()
                                        print(self.attacker + ", " + self.getting_attacked)
                                        self.gotdata = True

                                    if (player == 1):
                                        while (self.gotdata != True):
                                            time.sleep(0.1)
                                        if (self.gotdata):
                                            print(self.attacker + ", " + self.getting_attacked)
                                            time.sleep(1)
                                            client_socket.sendall(self.attacker.encode())
                                            time.sleep(1)
                                            client_socket.sendall(self.getting_attacked.encode())
                                            print("sent")
                                            self.attacker = ""
                                            self.getting_attacked = ""
                                    client_socket.sendall(self.data.encode())
                                    print("sent")
                                    print("next turn")
                                    self.data = ""
                                    self.gotdata = False
                                    break
                                else:
                                    client_socket.sendall(self.data.encode())
                                    print("sent")
                                if (str(self.data) == "exitexit32exitexit"):
                                    SQL_Data.Users().Update_user_characters(self.username, "0", "0", "0", "0", "0")
                                    SQL_Data.Users().Update_user_win(username)
                                    self.data = ""
                                    self.gotdata = False
                                    self.game(client_socket, username, player)
                                self.data = ""
                                self.gotdata = False
                        time.sleep(1)
                        if (player == 2 and x % 5 == 0 and str(self.data) != "2"):
                            self.attacker = client_socket.recv(1024).decode()
                            self.getting_attacked = client_socket.recv(1024).decode()
                            print(self.attacker + ", " + self.getting_attacked)
                            self.gotdata = True
                        if (player == 1 and x % 5 == 0 and str(self.data) != "2"):
                            while (self.gotdata != True):
                                time.sleep(0.1)
                            if (self.gotdata):
                                time.sleep(1)
                                client_socket.sendall(self.attacker.encode())
                                time.sleep(1)
                                client_socket.sendall(self.getting_attacked.encode())
                                print("sent")
                                self.attacker = ""
                                self.getting_attacked = ""
                    time.sleep(1)






        except socket.error as e:
            self.reset(player,username)
            print(e)




if __name__ == '__main__':
    hostname = socket.gethostname()
    ip = socket.gethostbyname(hostname)
    print(ip)
    port = 4242
    s = Server(ip, port)
    s.start()
