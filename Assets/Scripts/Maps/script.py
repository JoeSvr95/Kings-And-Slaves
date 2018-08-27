
from random import *

def validate_next_step(row, column, step):
    
    if column == 0 and (step == 1 or step == 2):
        return 1  # falso
    elif column == 3 and (step == 3 or step == 4):
        return 1  # falso
    elif row == 3 and step == 5:
        return -1  # terminar
    return 0  # correcto

def print_grid(grid):
    for i in grid:
        print(i)


def build_steps_matrix():

    ''' En esta matriz, sus valores determinan hacia donde se mueve "el puntero" 
        Si tiene un valor de 1 y 2, se mueve a la izq. De 3 y 4 a la derecha y 5 abajo'''
    steps_matrix = []
    dict_data = {}
    for i in range(4):
        steps_matrix.append([])
        for j in range(4):
            steps_matrix[i].append(0)

    actual_row = 0
    actual_column = 0
    initial_column = randint(0,3)
    actual_column = initial_column
    initial_step = randint(1,4)
    steps_matrix[actual_row][actual_column] = initial_step

    coordinate = str(actual_row) + str(actual_column)
    coordinates_asigned = []
    coordinates_asigned.append(coordinate)

    temp_column = actual_column
    temp_row = actual_row

    finish = False
    while not finish:
        next_step = randint(1, 5)
        validar = validate_next_step(actual_row, actual_column, next_step)
        if validar == 0:
            if next_step == 1 or next_step == 2:
                temp_column -= 1
            elif next_step == 3 or next_step == 4:
                temp_column += 1
            elif next_step == 5:
                temp_row += 1

            coordinate = str(temp_row) + str(temp_column)
            if coordinate not in coordinates_asigned:
                coordinates_asigned.append(coordinate)
                steps_matrix[actual_row][actual_column] = next_step
                actual_column = temp_column
                actual_row = temp_row
            else:
                temp_column = actual_column
                temp_row = actual_row
        
        elif validar == -1:
            finish = True
            steps_matrix[actual_row][actual_column] = 2

    print('path', end=': ')
    print(coordinates_asigned)
    print('Matriz: ')
    print_grid(steps_matrix)
    dict_data['path'] = coordinates_asigned
    dict_data['matrix'] = steps_matrix
    return dict_data      

# dict_data = build_steps_matrix()

def build_types_matrix(dict_data):
    ''' 
        Valores clase 1 -> izq y der
        Valores clase 2 -> '' '' '' y abajo
        Valores clase 3 -> '' '' '' y arriba
        Valores clase 4 -> Arriba y abajo
    '''
    matrix = []
    
    step_matrix = dict_data['matrix']

    for i in range(len(step_matrix)):
        new_row = []
        for j in range(len(step_matrix[i])):
            if i != 0:
                # chequeando que sobre o y tu no sean de tipo 5
                if step_matrix[i - 1][j] == 5:
                    if j != len(step_matrix) - 1:
                        if step_matrix[i][j] == 5:
                            new_row.append(4)
                        else:
                            new_row.append(3)
                else:
                    if step_matrix[i][j] == 5:
                        new_row.append(2)
                    elif step_matrix[i][j] == 0:
                        rand_casilla = randint(1,4)
                        new_row.append(rand_casilla)
                    else:    
                        new_row.append(1)
            else:
                if step_matrix[i][j] == 5:
                    new_row.append(2)
                elif step_matrix[i][j] == 0:
                    rand_casilla = randint(1,4)
                    new_row.append(rand_casilla)
                else:    
                    new_row.append(1)

        if len(new_row) != 4:
            rand_casilla = randint(1,4)
            new_row.append(rand_casilla)
        matrix.append(new_row)
    print('\n')
    print_grid(matrix)

    cadena = ""
    for i in matrix:
        for j in i:
            cadena += str(j) + ","

    primero = dict_data['path'][0]
    ultimo = dict_data['path'][-1] + "\n"

    cadena += primero + ','
    cadena += ultimo
    print(cadena)
    return cadena

 # build_types_matrix(dict_data)

def create_combinations():
    
    i = 0
    file = open('paths.csv', 'w')
    cadena = '00,01,02,03,10,12,13,20,21,22,23,30,31,32,33,initial,finish\n'
    file.write(cadena)
    combinations = []
    while i < 2000:
        dict_data = build_steps_matrix()
        cadena = build_types_matrix(dict_data)
        if cadena not in combinations:
            combinations.append(cadena)
            file.write(cadena)
            i += 1

    file.close()

create_combinations()