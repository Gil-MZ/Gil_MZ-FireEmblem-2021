import smtplib, random

def send_email(email):
    print(email)
    port = 587  # For SSL
    smtp_server = "smtp.gmail.com"
    sender_email = "CP9cyber@gmail.com"  # Enter your address
    receiver_email = str(email)  # Enter receiver address
    password = "CP9lucy123"
    rand = str(random.randint(0, 9))
    rand +=str(random.randint(0, 9))
    rand +=str(random.randint(0, 9))
    message = str(rand)
    print(type(message))
    server = smtplib.SMTP(smtp_server, port)
    server.starttls()
    server.login(sender_email, password)
    print("login success")
    server.sendmail(sender_email, receiver_email, message)
    print("Email has been sent to ")
    return str(rand)