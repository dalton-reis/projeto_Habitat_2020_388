using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using UnityEngine;

public class hcsr04 : MonoBehaviour
{
    SerialPort _serialPort;
    public float movimentoHorizontal = 0f;

    void Start()
    {
        ConectarDispositivo();
    }

    void Update()
    {
        try
        {
            int cm = _serialPort.ReadByte();
            print($"{cm} cm.");

            if (cm > 0 && cm < 10)
            {
                // mover para direita
                movimentoHorizontal = 1 * Time.deltaTime;
            }
            else if (cm > 10 && cm <= 20)
            {
                // mover para esqueda
                movimentoHorizontal = -1 * Time.deltaTime;
            }
            else
            {
                movimentoHorizontal = 0;
            }

            transform.Translate(movimentoHorizontal, 0, 0);

        }
        catch (Exception ex)
        {
            print("Dispositivo desconectado.");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            ConectarDispositivo();
        }
    }

    private void OnApplicationQuit()
    {
        _serialPort.Close();
    }

    private void ConectarDispositivo()
    {
        string[] ports = SerialPort.GetPortNames();

        if (!ports.Any())
            print("Nenhuma porta disponível!");

        foreach (string port in ports)
        {
            try
            {
                print($"Conectando: {port}");
                Thread.Sleep(TimeSpan.FromSeconds(3));
                _serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
                AbrirConexao();
            }
            catch (Exception ex)
            {
                print($"Porta {port} falhou: {ex.Message}");
            }
        }
    }

    private void AbrirConexao()
    {
        _serialPort.Open();
        _serialPort.ReadTimeout = 1000;
    }
}
