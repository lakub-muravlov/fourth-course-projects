from PIL import Image
 
def genData(data):
        newd = []
        for i in data:
            newd.append(format(ord(i), '08b'))
        return newd
 
def modPix(pix, data):
 
    datalist = genData(data)
    lendata = len(datalist)
    imdata = iter(pix)
 
    for i in range(lendata):
        pix = [value for value in imdata.__next__()[:3] +
                                imdata.__next__()[:3] +
                                imdata.__next__()[:3]]
        for j in range(0, 8):
            if (datalist[i][j] == '0' and pix[j]% 2 != 0):
                pix[j] -= 1
            elif (datalist[i][j] == '1' and pix[j] % 2 == 0):
                if(pix[j] != 0):
                    pix[j] -= 1
                else:
                    pix[j] += 1
        if (i == lendata - 1):
            if (pix[-1] % 2 == 0):
                if(pix[-1] != 0):
                    pix[-1] -= 1
                else:
                    pix[-1] += 1
        else:
            if (pix[-1] % 2 != 0):
                pix[-1] -= 1
        pix = tuple(pix)
        yield pix[0:3]
        yield pix[3:6]
        yield pix[6:9]
def encode_enc(newimg, data):
    w = newimg.size[0]
    (x, y) = (0, 0)
    for pixel in modPix(newimg.getdata(), data):
        newimg.putpixel((x, y), pixel)
        if (x == w - 1):
            x = 0
            y += 1
        else:
            x += 1
def encode():
    img = input("Enter path to container image: ")
    image = Image.open(img, 'r')
 
    text_path = input("Enter path to data to be encoded: ")
    
    data = ""
    with open(text_path, 'r') as inputFile:
        data = ' '.join(inputFile.readlines())

    newimg = image.copy()
    encode_enc(newimg, data)
 
    new_img_name = input("Enter path to new image: ")
    newimg.save(new_img_name, str(new_img_name.split(".")[1].upper()))
 
def decode():
    img = input("Enter path to container image: ")
    image = Image.open(img, 'r')
    data = ''
    imgdata = iter(image.getdata())
 
    while (True):
        pixels = [value for value in imgdata.__next__()[:3] +
                                imgdata.__next__()[:3] +
                                imgdata.__next__()[:3]]
        binstr = ''
        for i in pixels[:8]:
            if (i % 2 == 0):
                binstr += '0'
            else:
                binstr += '1'
        data += chr(int(binstr, 2))
        if (pixels[-1] % 2 != 0):
            return data

def main():
    a = int(input("Choose action type\n"
                        "1. Encode\n2. Decode\n"))
    if (a == 1):
        encode()
    elif (a == 2):
        print("Decoded Text: " + decode())
    else:
        raise Exception("Enter correct input")
 
if __name__ == '__main__' :
    main()