"""Customer DTO"""
import hashlib
import hmac
import base64
import psycopg2
import requests

SQL = """SELECT * from users where email = %s;"""

class SignInCustomerDTO():
	"""Used to authorize customers from the databse"""
	def __init__(self, email, enteredpassword):
		self.email = email
		self.enteredpassword = enteredpassword
		self.salt = ""
		self.password = ""
		self.fetched_password = ""
		self.user_id = 0

	def generateHashedPassword(self):
		"""Generates a hashed password bassed from the salt and entered password"""
		salt = bytes(self.salt, encoding = 'utf-8')
		password = bytes(self.enteredpassword, encoding = 'utf-8')
		self.password = base64.b64encode(hmac.new(salt, password, digestmod=hashlib.sha256).digest())

	def verifyPassword(self):
		"""Used to test the two passwords"""
		self.password = self.password.decode("utf-8")
		if(self.password == self.fetched_password):
			return True
		return False

	def fetchSaltAndPassword(self):
		"""Used to access the database to gather salt"""
		conn = psycopg2.connect(dbname="ratedrive", user="postgres", password="", host="172.19.0.1", port="4001")
		cur = conn.cursor()
		cur.execute(SQL, (self.email,))
		custs = cur.fetchall()
		for cust in custs:
			self.user_id = cust[0]
			self.salt = cust[3]
			self.fetched_password = cust[4]
		cur.close()
		conn.close()
