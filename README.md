# Integração Microcontrolador ESP32 + Sensor HC-SR04 com Unity

![image](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/demo-projeto.gif)

Este repositório pretende demostrar passo a passo como integrar um circuito gerenciado pelo microcontrolador ESP32 que recebe sinal de um sensor de distância ultrassônico HC-SR04 a uma aplicação Unity.

## Desenvolvimento

[Itens](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/itens.jpeg) utilizados.

```
- 1x Microcontrolador ESP32.
- 1x Sensor HC-SR04.
- 1x Protoboard 830 Furos MB102 Breadboard.
- 1x Cabo USB-Micro.
- 5x Fios para condução de energia, sendo um para o negativo.
```

Com todos os [itens](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/itens.jpeg) “em mãos” podemos iniciar a montagem do circuito.

1. Encaixe o microcontrolador ESP32 na Protoboard.
    * [exemplo](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/1p.jpeg).

2. Escolhas 4 fios (se possível de cores diferentes) e faça a conexão no sensor HC-SR04 nos pinos VCC, TRIG, ECHO, GND.
    * [exemplo](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/2p.jpeg).
    * [exemplo](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/3p.jpeg).

3. Recomendo que o sistema de [PINOUT](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/pinout.png) do microcontrolador ESP32 seja consultado.
    * [Download PINOUT.pdf](http://wiki.amperka.ru/_media/products:esp32-wroom-wifi-devkit-v1:esp32-wroom-wifi-devkit-v1_pinout.pdf)

4. Conecte uma das portas GND do microcontrolador na indução do negativo.
    * [exemplo](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/4p.jpeg).

5. Faça a pinagem que conecta o sensor (HC-SR04) ao microcontrolador (ESP32) da seguinte forma:
   * O pino VCC do sensor deve ser interligado na porta 5V do microcontrolador.

   * Para controle de I/O (INPUT, OUTPUT) os pinos TRIG e ECHO do sensor devem ser interligados nas portas 26 e 27 do microcontrolador, respectivamente.

   * O pino GND do sensor deve ser interligado na indução de negativo na Protoboard.

   * [exemplo](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/5p.jpeg).

   * [exemplo](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/6p.jpeg).

   * [exemplo](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/7p.jpeg).

6. Você deve chegar no seguinte [resultado](https://github.com/anderson-guimaraes/furb-integracao-esp32/blob/main/img/8p.jpeg).

Agora que o circuito está montado, você pode optar por implementar o código de forma "[simples](#Implementação-Simples)" ou "[avançada](#Implementação-Avançada)".

## Implementação Simples

Para esse modo o [Arduino IDE](https://www.arduino.cc/en/Main/Software_) deve ser instalado e os seguintes passos seguidos.

 1. Na IDE, siga para "Arquivo > Preferências > Configurações" ou "ctrl + ," e em "URLs Adicionais para Gerenciador de Placas" adicione:
    * <http://arduino.esp8266.com/stable/package_esp8266com_index.json,https://dl.espressif.com/dl/package_esp32_index.json>

 2. Na IDE, siga para "Ferramentas > Placa: ... > Gerenciador de Placas..." e busque por "ESP32", verifique o resultado da busca e instale o pacote se necessário.

 3. Na IDE, siga para "Ferramentas > Placa: ..." e selecione a "DOIT ESP32 DEVKIT V1" instalado no pacote anteriormente.

 4. Conecte o circuito montado ao computador utilizando o cabo Micro-USB e verifique no seu gerenciador de dispositivo em qual porta está alocado.

 5. Siga para "Ferramentas > Portas: ..." e selecione a porta alocada disponível.

 6. Obtenha o código [aqui](https://github.com/anderson-guimaraes/furb-integracao-esp32/tree/main/arduino) e implemente.

 7. Faça deploy do código para dentro dos microcontrolador.
    * Certifique-se que o circuito está conectado ao computador.

    * Clique no botão "Verificar" da IDE e veja se o código implementado não possui falhas.

    * Clique no botão "Carregar" da IDE e aguarde pela mensagem "Hard resetting via RTS pin..." exibida no prompt.

    * Procure na parte superior do microcontrolador pelo botão “EN” e o mantenha pressionado por aproximadamente 5 segundos.

    * Abra o monitor serial da IDE (canto superior direito) e selecione a velocidade 9600. A partir desse momento já é possível ver os dados captados pelo sensor.

 8. Faça a integração com o [Unity](#Unity).

## Implementação Avançada
Para esse modo o [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) deve ser instalado e os seguintes passos seguidos.

1. Baixe e instale os SDKs versão .NET 3.1 e .NET 5.0 [aqui](https://dotnet.microsoft.com/download).

2. Adicione a extensão do [nanoFramework](https://marketplace.visualstudio.com/items?itemName=nanoframework.nanoFramework-VS2019-Extension) ao seu VS19, o processo pode ser feito diretamente na IDE.

3. Obtenha o projeto [aqui](https://github.com/anderson-guimaraes/furb-integracao-esp32/tree/main/nanoframework/NFApp1) e abra na IDE com permissões administrativas.

4. Na IDE siga para "View > Other Windows" e abra o "Package Manager Console".

5. Execute os comandos, em caso de falha execute via "CMD" ou "PowerShell" com permissões elevadas:
    * dotnet tool install -g nanoFirmwareFlasher
    * dotnet tool update -g nanoFirmwareFlasher

6. Conecte o circuito ao seu computador utilizando o cabo Micro-USB.
    * Abra seu gerenciador de dispositivos e verifique em qual "COM" está alocado.

7. Faça deploy do código para dentro dos microcontrolador.
    * Abra o Package Manager Console e execute, substituindo "#" pelo número da sua COM:  
     nanoff --update --target ESP32_WROOM_32 --serialport COM#  
     Se preferir, também é possível clicando com botão direito sobre a solução do projeto escolhendo "Deploy Solution".

    * Aguarde o deploy até identificar no Package Manager Console que o envio foi realizado com sucesso e que é necessário "Hard resetting via RTS pin...".

    * Procure na parte superior do microcontrolador pelo botão “EN” e o mantenha pressionado por aproximadamente 5 segundos.

    * Desconecte e conecte novamente o dispositivo.

    * Execute a depuração (F5) do projeto e abra o "output" da IDE em "View > Output" ou "ctrl + w + o". A partir desse momento já é possível ver os dados captados pelo sensor.

8. Faça a integração com o [Unity](#Unity).

## Unity

Baixe o [Unity HUB](https://unity.com/pt/download) e instale a versão "2018.4.29f1" LTS.

* Obtenha o projeto [aqui](https://github.com/anderson-guimaraes/furb-integracao-esp32/tree/main/unity/demo-esp32) e abra na IDE.

* Conecte o circuito montado ao computador utilizando o cabo Micro-USB.

* Ao executar a aplicação deverá ser exibido no console algumas informações de conectividade com o circuito.

* Distâncias entre 0 ~ 10 cm movem o cubo para a direita.

* Distâncias entre 10 ~ 20 cm movem o cubo para a esqueda.

## Conclusão

Orientador:

* **Dalton Solano dos Reis**.

Colaborador:

* **Miguel Alexandre Wisintainer**.

Desenvolvedor:

* **Anderson Guimarães** [Github](https://github.com/anderson-guimaraes)