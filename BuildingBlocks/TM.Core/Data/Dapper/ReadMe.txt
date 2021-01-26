ConnectionBase 为dapper的包装。

dapper的读写分离要自行实现，目前仅写了部分。不想写了，写吐了。
临时改用 sqlSugar来做ORM

如果使用阿里云的POLARDB MySql版的话，POLARDB因支持自动读写分离，可以不用在代码层面做读写分离。
但是 分区分表需要自行实现。

POLARDB的好处：

高性能比自建mysql快6倍
读一致性
弹性扩容存储