# 🃏 WCF Multiplayer Blackjack

![C#](https://img.shields.io/badge/C%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D6?style=for-the-badge&logo=windows&logoColor=white)

Багатокористувацька настільна гра «Блекджек» (Blackjack) із підтримкою ігрових кімнат (лобі) та текстового чату в реальному часі. Проект побудований на базі **Windows Communication Foundation (WCF)** з використанням дуплексного зв'язку (Duplex Contracts) для миттєвої синхронізації стану гри між сервером та всіма підключеними клієнтами.

## 🎮 Ключовий функціонал
* **Система Лобі:** Можливість створювати власні ігрові кімнати або приєднуватися до існуючих.
* **Реал-тайм мультиплеєр:** Всі гравці за столом бачать дії один одного, карти та результати без затримок.
* **Динамічний ігровий стіл:** Програмна генерація інтерфейсу (візуалізація карт, аватарів гравців та дилера) за допомогою GDI+ у WinForms.
* **Розумний Дилер (AI):** Автоматичний хід дилера за класичними правилами казино (бере карти до 17).
* **Вбудований чат:** Віджет чату поверх ігрового столу з підтримкою кольорових мастей карт (♥, ♦, ♣, ♠) та можливістю перетягування (Drag-and-Drop).

## 🛠 Стек технологій
* **Мова:** C# 
* **Платформа:** .NET Framework
* **Мережева взаємодія:** WCF (NetTcpBinding, DuplexChannel)
* **Інтерфейс користувача:** Windows Forms (Custom Paint, FlowLayoutPanel)
* **Асинхронність:** Task Parallel Library (TPL) для неблокуючих ігрових таймерів.