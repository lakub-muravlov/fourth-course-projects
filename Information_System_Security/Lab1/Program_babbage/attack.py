import babbage
import processing
import freq_analysis as fa
from const import SEQ_LEN
import string


def _decypher(cyphertext, key):
    letters = string.ascii_uppercase
    shifts = [letters.index(letter) for letter in key]
    blocks = processing.get_blocks(text=cyphertext,size=len(key))
    cols = processing.get_columns(blocks)
    decyphered_blocks = processing.to_blocks([fa.shift(col, shift) for col, shift in zip(cols, shifts)])
    decyphered = ''.join(decyphered_blocks)
    return decyphered


def attack(file):
    with open(file) as f:
        cyphertext = f.readlines()
        key_len = 0
        print('starting babbage attack')
        key_len = babbage.find_key_length(cyphertext=cyphertext[0], seq_len=SEQ_LEN)
        key = fa.restore_key(cyphertext[0], key_len)
        decyphered = _decypher(cyphertext[0], key)
        print('Chosen key length: '+str(key_len))
        print('Restored key: '+str(key))
        print('Plaintext: '+str(decyphered))



attack('hostiletext.txt')
