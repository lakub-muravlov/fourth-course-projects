from vigenere import vigenere

txt="Hello world!"
q = vigenere(txt, 'MURAVLOV', 'e')
print(q, '\n')
print(vigenere(q, 'MURAVLOV', 'd'))
