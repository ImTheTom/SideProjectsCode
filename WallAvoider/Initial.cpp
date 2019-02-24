#include <Arduino.h>
#include <SoftwareSerial.h>

// https://www.arduino.cc/en/Reference/PortManipulation
// https://www.arduino.cc/en/Hacking/Atmega168Hardware
// PORTD &= ~(1 << n); Pin n goes low
// PORTD |= (1 << n); Pin n goes high

long duration;
int distance;
int randomDigit;
bool automatic = false;

SoftwareSerial blueTooth(0, 1);

void initialise();
void run();

int get_distance();
int random_delay();
void drive();

void motor_one_forward();
void motor_two_forward();
void motor_one_backwards();
void motor_two_backwards();
void motor_one_stop();
void motor_two_stop();

void go_forward();
void go_backwards();
void go_left();
void go_right();
void go_stop();

void motor_one_forward(){
  PORTD |= (1 << 4);
  PORTD &= ~(1 << 5);
  PORTB |= (1 << 1);
}

void motor_two_forward(){
  PORTD |= (1 << 7);
  PORTD &= ~(1 << 6);
  PORTB |= (1 << 2);
}

void motor_one_backwards(){
  PORTD &= ~(1 << 4);
  PORTD |= (1 << 5);
  PORTB |= (1 << 1);
}

void motor_two_backwards(){
  PORTD &= ~(1 << 7);
  PORTD |= (1 << 6);
  PORTB |= (1 << 2);
}

void motor_one_stop(){
  PORTD &= ~(1 << 4);
  PORTD &= ~(1 << 5);
  PORTB &= ~(1 << 1);
}

void motor_two_stop(){
  PORTD &= ~(1 << 6);
  PORTD &= ~(1 << 7);
  PORTB &= ~(1 << 2);
}

void go_forward(){
  motor_one_forward();
  motor_two_forward();
}

void go_backwards(){
  motor_one_backwards();
  motor_two_backwards();
}

void go_left(){
  motor_one_backwards();
  motor_two_forward();
}

void go_right(){
  motor_one_forward();
  motor_two_backwards();
}

void go_stop(){
  motor_one_stop();
  motor_two_stop();
}

void initialise(){
  DDRD |= 0b11110100; // 1,2,4,5,6,7 out 3,0 in
  DDRB |= 0b00000110; // 9 10 out
  Serial.begin(9600);
  blueTooth.begin(9600);
  Serial.println("Started...");
}

int get_distance(){
  PORTD &= ~(1 << 2);
  delayMicroseconds(2);
  PORTD |= (1 << 2);
  delayMicroseconds(10);
  PORTD &= ~(1 << 2);
  duration = pulseIn(3, HIGH);
  return duration*0.034/2;
}

int random_delay(){
  randomDigit = random(400);
  return randomDigit+400;
}

void drive() {
  distance = get_distance();
  if(distance < 30) {
    go_backwards();
    delay(random_delay());
    randomDigit = random(2);
    if(randomDigit==1){
      go_left();
      delay(random_delay());
    }else{
      go_right();
      delay(random_delay());
    }
    go_forward();
  }
}

void run() {
  get_distance();
  if(Serial.available()) {
    char data = Serial.read();
    Serial.println(data);
    switch(data){
      case '2': //2 Forward
        go_forward();
        break;
      case '8': //8 Back
        go_backwards();
        break;
      case '6': //6 Right
        go_right();
        break;
      case '4': //4 Left
        go_left();
        break;
      case '5': //5 Stop
        go_stop();
        break;
      case '1': //1 automatic
        go_forward();
        automatic = !automatic;
        break;
    }
    delay(150);
  }
  
  if(automatic) {
    drive();
  }
}

int main () {
  init();
  initialise();
  while (true)
    run();
}