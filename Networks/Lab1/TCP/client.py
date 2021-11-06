import time
import socket
import pickle
import random
import string

HOST = "127.0.0.1"
PORT = 8080

intArr = []
charArr = []

for i in range(0,7):
    if(i<5):
        intArr.append(random.randrange(0,100))
        charArr.append((random.choice(string.ascii_letters)))
    else:
        charArr.append((random.choice(string.ascii_letters)))

dataToSend = intArr + charArr

SOCKET = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
SOCKET.connect((HOST,PORT))

for item in dataToSend:
    SOCKET.send(pickle.dumps(item))
    print(f"{item} — sent")
    time.sleep(0.5)

time.sleep(1)
SOCKET.close()