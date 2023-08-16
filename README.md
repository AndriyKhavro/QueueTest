## Run the tests
```
.\run.ps1
```

## Results

| Queue     | Persistance | Writes (min) | Reads (min) |
|-------------------------|--------------|-------------|
| Redis     | rdb         |  119766      |  119892     |
| Redis     | aot         |  113963      |  114860     |
| Redis     | no          |  121360      |  121675     |
| Beanstalk | yes         |  139534      |  69943      |
| Beanstalk | no          |  140058      |  70271      |