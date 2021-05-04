//TaskHandle_t tarefa3;

int trigPin = 26; //saida
int echoPin = 27; //entrada

void setup() {
  Serial.begin (9600);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
}

void loop() {

  int distance;
  long duration;
  digitalWrite(trigPin, LOW); // Added this line
  delayMicroseconds(2); // Added this line
  digitalWrite(trigPin, HIGH);
  // delayMicroseconds(1000); – Removed this line
  delayMicroseconds(10); // Added this line
  digitalWrite(trigPin, LOW);
  duration = pulseIn(echoPin, HIGH);
  distance = (duration / 2) / 29.1;

  int val = 2;

  //Serial.println(distance);

  if (distance >= 1211) {
    Serial.write(0);
  } else {
    Serial.write(distance);
  }
  delay(200);
}
