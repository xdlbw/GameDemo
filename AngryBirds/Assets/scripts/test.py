import base64

"
i=1
while(i<=50){
    b64 = str(base64.standard_b64decode(a), 'utf-8')
}


while 'easyctf' not in b64:
    b64 = str(base64.standard_b64decode(b64), 'utf-8')
print(b64)