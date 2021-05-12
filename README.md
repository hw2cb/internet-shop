# internet-shop
asp.net framework mvc
создаешь базу данных новую, которая содержит две таблицы
В первой пропишешь этот код:
CREATE TABLE [dbo].[Orders] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [Surname]     NVARCHAR (50) NULL,
    [Patronymic]  NVARCHAR (50) NULL,
    [Phone]       NVARCHAR (50) NULL,
    [Email]       NVARCHAR (50) NULL,
    [ProductID]   INT           NULL,
    [Quantity]    INT           NULL,
    [OrderNumber] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
Во второй пропишешь этот код:
CREATE TABLE [dbo].[Products] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [Price]       DECIMAL (18)  NULL,
    [Quantity]    INT           NULL,
    [Description] NVARCHAR (60) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
это прописать снизу где T-SQL
Далее в Web.config пропиши строку подключения, взять ее можно создав новый проект прямо в этом (правой кнопкой мыши по решению справа, добавить ->создать проект...)
Создать новый проект нужно Классическое приложение Windows, приложение WindowsForms (.NET Framework)
Далее жмем сверху Вид -> Другие окна -> Источники данных, слева нажмешь Добавить новый источник данных...
База данных -> Набор данных -> Галку на "Показать строку подключения" и жмем Далее 
(ВАЖНО! может вылезти окно, в которой говорится:
Выбранное подключение использует локальный файл данных, не относящийся к текущему проекту ....... если такое вылезло, то жми НЕТ)
Потом галку на Да, сохранить подключение как: , жмешь Далее, ждешь, ставишь галку на Таблицы и жмешь Готово
После всего этого, в этом проекте жмешь на файл App.config, там ищешь <connectionStrings> и все копируешь до </connectionStrings>
Все, теперь идем в наш старый проект с магазином, в Web.config вставишь вместо моей строки свою. После этого можешь удалять проект WindowsForms
Теперь база должна работать, если все сделано верно
