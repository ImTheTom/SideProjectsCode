from csv import reader

def load_csv(filename):
    file = open(filename, "r")
    lines = reader(file)
    dataset = list(lines)
    return dataset

def str_column_to_float(dataset, column):
    for row in dataset:
        row[column] = float(row[column].strip())

def str_column_to_int(dataset, column):
    class_values = [row[column] for row in dataset]
    unqiue_values = set(class_values)
    lookup = dict()
    for i, value in enumerate(unqiue_values):
        lookup[value] = i
    for row in dataset:
        row[column] = lookup[row[column]]
    return lookup

class DataCreate():
    dataset = []

    def __init__(self, filename, columns_to_float=[], columns_to_int=[]):
        self.dataset = load_csv(filename)
        if(len(columns_to_float)!=0):
            for i in columns_to_float:
                str_column_to_float(self.dataset, i)
        if(len(columns_to_int)!=0):
            for i in columns_to_int:
                str_column_to_int(self.dataset, i)

    def get_data(self):
        return self.dataset