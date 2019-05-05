FROM python:3.8.0a3-stretch

COPY requirements.txt /app/requirements.txt

# Set the working directory to /app
WORKDIR /app

# Install any needed packages specified in requirements.txt
RUN pip install --trusted-host pypi.python.org -r requirements.txt

# Copy the current directory contents into the container at /app
COPY . /app

# Make port 80 available to the world outside this container
EXPOSE 80

# Define environment variable
ENV NAME World

# Run app.py when the container launches
CMD ["gunicorn", "--reload", "-b", "0.0.0.0:80", "app:api"]