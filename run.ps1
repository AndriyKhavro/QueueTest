docker compose up -d

function RunWriteAndReadInParallel {
    param (
        [string]$ClientCode,
        [int]$Port
    )

    $process1 = Start-Process -FilePath "dotnet" -ArgumentList "run -c Release --project QueueTest.Write/QueueTest.Write.csproj $ClientCode $Port" -PassThru
    $process2 = Start-Process -FilePath "dotnet" -ArgumentList "run -c Release --project QueueTest.Read/QueueTest.Read.csproj $ClientCode $Port" -PassThru

    $process1.WaitForExit()
    $process2.WaitForExit()
}

RunWriteAndReadInParallel Redis 6379
RunWriteAndReadInParallel Redis 6380
RunWriteAndReadInParallel Redis 6381
RunWriteAndReadInParallel Beanstalk 11300
RunWriteAndReadInParallel Beanstalk 11301
