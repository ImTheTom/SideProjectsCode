# -*- coding: utf-8 -*-
try:
    from setuptools import setup
except ImportError:
    from distutils.core import setup
from codecs import open
from sys import exit,version
import sys

if version < '3.0.0':
    print("Python 2 is not supported...")
    sys.exit(1)

setup(
    name='coinget',
    version='1.1',
    author='Tom Bowyer',
    author_email='tom.bowyer4310@gmail.com',
    url = 'https://github.com/ImTheTom/CoinGet',
    license='BSD',
    description = 'Get cryptocurrency data from command line',
    include_package_data=True,
    packages = ["coinget"],
    entry_points = {"console_scripts": ['coinget = coinget.main:run', 'coingetkey = coinget.startup:run']},
    data_files=[('coinget', ['coinget//key.txt', 'coinget//coins.txt', 'coinget//assets.pkl'])]
)
