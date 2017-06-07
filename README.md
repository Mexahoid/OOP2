# OOP2

4. Смоделировать работу зала с банкоматами.

В зале N (задается в программе) банкоматов. Люди заходят в зал случайным образом, интервалы времени между появлением покупателей распределены по нормальному закону.

Когда клиенты подходят к банкоматам, они занимают очередь в случайный банкомат. Длительность обслуживания постоянна. Визуализировать работу зала(как минимум кол - во людей в зале и на каждом банкомате).

Поведение каждого клиента и каждого банкомата реализуется в отдельном потоке(соответствующим образом должны быть описаны классы клиента и банкомата). 

Когда клиент становится к банкомату (вызывается соответствующий метод «Взять бабло»), поток клиента блокируется до момента, пока банкомат не освободится (синхронизация с банкоматом с помощью нужного EventWaitHandle). 

Задержки выдачи денег реализуется с помощью Thread.Sleep(). При желании можно воспользоваться классами из пространства имен System.Threading.Tasks.
