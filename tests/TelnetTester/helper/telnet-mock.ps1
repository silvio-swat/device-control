param(
    [switch]$Verbose
)

# Função auxiliar para gerar respostas JSON formatadas
function CreateResponse {
    param (
        [string]$key,
        [string]$value
    )
    return "{`"$key`":`"$value`"}`r"
}

# Loop para ler cada linha até o encerramento da conexão
while ($true) {
    $line = [Console]::In.ReadLine()
    if ($null -eq $line) { break }

    # Remover eventual \r ou \n
    $line = $line.TrimEnd("`r", "`n")

    # Extrai comando e parâmetros
    $parts = $line.Split(" ", 3)
    $cmd   = $parts[0]
    $deviceId = if ($parts.Count -gt 1) { $parts[1] } else { "" }
    $param = if ($parts.Count -gt 2) { $parts[2] } else { "" }

    # Verifica o dispositivo e comando
    switch ("$deviceId|$cmd".ToUpper()) {

        # Device 001 - Sensor de Temperatura e Umidade
        "DEVICE001|READ_TEMP" {
            $unit = if ($param) { $param } else { "C" }
            $resp = CreateResponse "temperature" "23.4 $unit"
        }
        "DEVICE001|READ_HUM" {
            $resp = CreateResponse "humidity" "65%"
        }

        # Device 002 - Sensor de Pressão Atmosférica
        "DEVICE002|READ_PRESSURE" {
            $unit = if ($param) { $param } else { "hPa" }
            $resp = CreateResponse "pressure" "1013 $unit"
        }
        "DEVICE002|READ_ALTITUDE" {
            $resp = CreateResponse "altitude" "350m"
        }

        # Device 003 - Controlador de Iluminação LED
        "DEVICE003|SET_BRIGHTNESS" {
            $level = if ($param) { $param } else { "50" }
            $resp = CreateResponse "brightness" "$level%"
        }
        "DEVICE003|READ_POWER" {
            $resp = CreateResponse "power" "15W"
        }
        "DEVICE003|TOGGLE_POWER" {
            $resp = CreateResponse "state" "ON"
        }

        # Comando desconhecido ou erro
        Default {
            $resp = CreateResponse "error" "Unknown command or device"
        }
    }

    # Escreve na saída padrão (ncat encaminha para o cliente)
    [Console]::Out.Write($resp)
}
