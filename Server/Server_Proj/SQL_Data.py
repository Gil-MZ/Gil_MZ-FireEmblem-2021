import sqlite3

class Users:
    """Creates database with users table includes:
       create query
       insert query
       select query
    """

    def __init__(self, tablename = "users", userId = "userId", Email = "Email", password = "password", username = "username"
                 , win = "win", lose = "lose", c1 = "c1", c2 = "c2", c3 = "c3", c4 = "c4", c5 = "c5", online = "online"):
        self.__tablename = tablename
        self.__userId = userId
        self.__Email = Email
        self.__password = password
        self.__username = username
        self.__win = win
        self.__lose = lose
        self.__c1 = c1
        self.__c2 = c2
        self.__c3 = c3
        self.__c4 = c4
        self.__c5 = c5
        self.__online = online
        conn = sqlite3.connect('Database.db')
        query_str = "CREATE TABLE IF NOT EXISTS " + tablename + "(" + self.__userId + " " + \
                    " INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,"
        query_str += " " + self.__Email + " TEXT    NOT NULL    UNIQUE ,"
        query_str += " " + self.__password + " TEXT    NOT NULL ,"
        query_str += " " + self.__username + " TEXT    NOT NULL    UNIQUE ,"
        query_str += " " + self.__win + " TEXT    NOT NULL ,"
        query_str += " " + self.__lose + " TEXT    NOT NULL ,"
        query_str += " " + self.__c1 + " TEXT    NOT NULL ,"
        query_str += " " + self.__c2 + " TEXT    NOT NULL ,"
        query_str += " " + self.__c3 + " TEXT    NOT NULL ,"
        query_str += " " + self.__c4 + " TEXT    NOT NULL ,"
        query_str += " " + self.__c5 + " TEXT    NOT NULL ,"
        query_str += " " + self.__online + " TEXT    NOT NULL );"

        conn.execute(query_str)
        conn.commit()
        conn.close()

    def get_table_name(self):
        print("table")
        return self.__tablename

    def insert_user(self, email, username, password):
        print("insert")
        conn = sqlite3.connect('Database.db')
        insert_query = "INSERT INTO " + self.__tablename + " (" + self.__Email + "," + self.__password + "," + self.__username \
                       + "," +self.__win + "," +self.__lose + "," +self.__c1 + "," +self.__c2 + "," +self.__c3 + "," \
                       +self.__c4 + "," +self.__c5 + "," + self.__online + ") VALUES " + "(" + "'" + email + "'"+ "," + "'" + password \
                       + "'" + "," + "'" + username + "'" + "," + "0" + "," + "0" + "," + "0" + "," + "0" +\
                       "," + "0" + "," + "0" + "," + "0" + "," + "0" + ");"
        conn.execute(insert_query)
        conn.commit()
        conn.close()
        print("Record created successfully")

    def delete_user(self, id):
        conn = sqlite3.connect('Database.db')

        delete_query = "DELETE FROM " + self.get_table_name() + " WHERE " + self.__userId+ " = " + str(id)
        print(delete_query)
        conn.execute(delete_query)
        conn.commit()
        conn.close()
        print("record has been deleted")

    def getwin(self, username):
        print("win")
        conn = sqlite3.connect('Database.db')
        str1 = "select * from users;"
        cursor = conn.execute(str1)
        for row in cursor:
            if username == str(row[3]):
                conn.close()
                return str(row[4])
        print("Operation done successfully")
        conn.close()

    def getlose(self, username):
        print("lose")
        conn = sqlite3.connect('Database.db')
        str1 = "select * from users;"
        cursor = conn.execute(str1)
        for row in cursor:
            if username == str(row[3]):
                conn.close()
                return str(row[5])
        print("Operation done successfully")
        conn.close()
        return "0"

    def getcharacters(self,username):
        print("get_character")
        conn = sqlite3.connect('Database.db')
        str1 = "select * from users;"
        list_User = []
        cursor = conn.execute(str1)
        for row in cursor:
            if(row[9] != 0 and username != row[3] and str(row[11]) == "True"):
                list_User.append(row[6])
                list_User.append(row[7])
                list_User.append(row[8])
                list_User.append(row[9])
                list_User.append(row[10])
                return list_User

        return list_User

    def Update_user_username(self, id1, username):
        conn = sqlite3.connect('Database.db')

        update_query = "UPDATE " + self.get_table_name() + " SET "\
                       + self.__password + " = " + "\' " +str(username)+" \' " + " WHERE " + self.__userId + " = " + str(id1)+";"
        conn.execute(update_query)
        conn.commit()
        conn.close()
        print("record has been updated")


    def Update_user_characters(self, username, c1, c2, c3, c4, c5):
        print("character")
        conn = sqlite3.connect('Database.db')
        conn.execute("UPDATE users SET c1=?, c2=?, c3=?, c4=?, c5=? WHERE username=?", (c1, c2, c3, c4, c5, username))
        conn.commit()
        conn.close()
        print("record has been updated")

    def Update_user_online(self, username, online):
        conn = sqlite3.connect('Database.db')
        conn.execute("UPDATE users SET online = ? WHERE username = ?", (online, username))
        conn.commit()
        conn.close()
        print("record has been updated")

    def Update_user_password(self, id1, password):
        conn = sqlite3.connect('Database.db')

        update_query = "UPDATE " + self.get_table_name() + " SET "\
                       + self.__username + " = " + "\' " +str(password)+" \' " + " WHERE " + self.__userId + " = " + str(id1)+";"
        conn.execute(update_query)
        conn.commit()
        conn.close()
        print("record has been updated")

    def Update_user_win(self,username):
        conn = sqlite3.connect('Database.db')
        conn.execute("UPDATE users SET win = ? WHERE username = ?", (str(int(self.getwin(username))+1), username))
        conn.commit()
        conn.close()
        print("record has been updated")

    def Update_user_lose(self,username):
        conn = sqlite3.connect('Database.db')
        conn.execute("UPDATE users SET lose = ? WHERE username = ?", (str(int(self.getlose(username)) + 1), username))
        conn.commit()
        conn.close()
        print("record has been updated")

    def select_user_by_User(self, email, username, password, access):
        print("select")
        conn = sqlite3.connect('Database.db')
        str1 = "select * from users;"
        list_User = []
        cursor = conn.execute(str1)
        try:
            for row in cursor:
                list_User.append(row[0])
                list_User.append(row[1])
                list_User.append(row[2])
                list_User.append(row[3])
                list_User.append(row[11])
            if (access == 2):
                print(password)
                if (email in list_User):
                    return True
                elif(password == username or len(str(password)) < 6):
                    return True
                elif (username in list_User):
                    return True
                elif (len(username) < 5):
                    return True
                elif (len(username) > 13):
                    return True
                elif (username.find(" ") != -1):
                    return True
            else:
                print(str(list_User[list_User.index(username)-2]) == email)
                if(str(list_User[list_User.index(username)+1]) == "0" and email == str(list_User[list_User.index(username)-2])
                        and username in list_User and list_User[list_User.index(username)-1] == password):
                    print("YAY")
                    return True
                print("No")
                return False
        except:
            return False




u = Users()
