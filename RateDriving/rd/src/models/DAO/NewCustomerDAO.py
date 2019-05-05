"""New customer DAO"""
import psycopg2

SQL = """INSERT INTO users(username, email, salt, password, created_on)
             VALUES(%s, %s, %s, %s, %s);"""

SQL2 = """SELECT * from users where username = %s;"""

SQL3 = """SELECT * from users where email = %s;"""

class NewCustomerDAO():
	"""Used to create new customers"""
	def __init__(self, name, email, salt, password, date_created):
		self.name = name
		self.email = email
		self.salt = salt
		self.password = password
		self.date_created = date_created

	def verify(self):
		"""Used to verify user can be created"""
		conn = psycopg2.connect(dbname="ratedrive", user="postgres", password="", host="172.19.0.1", port="4001")
		cur = conn.cursor()
		cur.execute(SQL2, (self.name,))
		custs = cur.fetchall()
		if(len(custs) != 0):
			return False
		cur.execute(SQL3, (self.email,))
		custs = cur.fetchall()
		if(len(custs) != 0):
			return False
		cur.close()
		conn.close()
		return True

	def save(self):
		"""Used to save user into database"""
		conn = psycopg2.connect(dbname="ratedrive", user="postgres", password="", host="172.19.0.1", port="4001")
		cur = conn.cursor()
		cur.execute(SQL, (self.name,self.email,self.salt,self.password,self.date_created,))
		conn.commit()
		cur.close()
		conn.close()
