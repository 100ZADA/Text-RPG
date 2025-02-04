using System;
using System.Collections.Generic;

internal class Program
{
    // 캐릭터 정보 클래스
    class Player
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Chad";
        public string Job { get; set; } = "전사";
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        public int Health { get; set; } = 100;
        public int Gold { get; set; } = 1500;

        public Player(string name = "Chad", string job = "전사")
        {
            Name = name;
            Job = job;
        }

        // 캐릭터 인터페이스 정보
        public void DisplayInfo()
        {
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체 력 : {Health}");
            Console.WriteLine($"Gold : {Gold} G");

            Console.WriteLine("\n 0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
        }
    } 

    // 인벤토리 클래스
    public class Inventory
    {
        private List<Item> items;  // 아이템 목록

        public Inventory()
        {
            items = new List<Item>();  // 아이템 목록 초기화
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"{item.ItemName}");          // 아이템이 인벤토리에 추가
        }

        public void InventoryInfo()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            if(items.Count == 0)
            {
                Console.WriteLine("[아이템 목록]\n");
            }
            else
            {
                Console.WriteLine("[아이템 목록]\n");
                
                foreach (var item in items)
                {
                    string euipped = item.Equipped ? "[E]" : " ";       // 아이템 장착 여부

                    Console.WriteLine($"- {(item.Equipped ? "[E]" : "   ")}{item.ItemName} | {(item.Attack > 0 ? $"공격력 +{item.Attack}" : "")}" +
                        $"{(item.Defense > 0 ? $" 방어력 +{item.Defense}" : "")} | {item.ItemInfo}");
                }
            }
            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("행동을 입력해주세요.\n>>\n");
        }
        
        // 장비 장착 관리
        public void ManageEquipment()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            if(items.Count == 0)
            {
                Console.WriteLine("[아이템 목록]\n");
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                string equipped = items[i].Equipped ? "[E]" : "   ";
                Console.WriteLine($"{i + 1}. {(items[i].Equipped ? "[E]" : "   ")}{items[i].ItemName} | {(items[i].Attack > 0 ? $"공격력 +{items[i].Attack}" : "")}" +
                    $"{(items[i].Defense > 0 ? $" 방어력 +{items[i].Defense}" : "")} | {items[i].ItemInfo}");

            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= items.Count)
            {
                var selectedItem = items[choice - 1];
                if (selectedItem.Equipped)
                {
                    selectedItem.Equipped = false; // 장착 해제
                    Console.WriteLine($"{selectedItem.ItemName}이(가) 장착 해제되었습니다.");
                }
                else
                {
                    selectedItem.Equipped = true; // 장착
                    Console.WriteLine($"{selectedItem.ItemName}이(가) 장착되었습니다.");
                }
            }
            else if (choice == 0)
            {
                Console.WriteLine("장착 관리에서 나갑니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    // 아이템 정보 클래스
    public class Item
    {
        public string ItemName { get; set; }    // 아이템 이름
        public string ItemInfo { get; set; }    // 아이템 설명
        public int Attack {  get; set; }        // 아이템 공격력
        public int Defense { get; set; }         // 아이템 방어력
        public int Gold { get; set; }           // 아이템 가격
        public bool Purchase { get; set; }      // 아이템 구매 여부
        public bool Equipped {  get; set; }     // 아이템 장착 여부

        public Item(string itemName, string itemInfo, int attack, int defense, int gold)     // 아이템 클래스 생성자
        {
            ItemName = itemName;
            ItemInfo = itemInfo;
            Attack = attack;
            Defense = defense;
            Gold = gold;
            Purchase = false;                   // 아이템이 존재하지 않을 때 구매
            Equipped = false;                   // 아이템을 장착하지 않았을 때
        }
    }

    // 상점 클래스
    public class Shop
    {
        private List<Item> shopItems;
      

        public Shop()
        {
            shopItems = new List<Item>
            {
                new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 1000),
                new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 2000),
                new Item("스파르타의 갑옷", "스파르타 전사들이 사용했던 전설의 갑옷입니다.", 0, 15, 3500),
                new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 600),
                new Item("청동 도끼", "어디선가 사용됐던 것 같은 도끼입니다.", 5, 0, 1500),
                new Item("스파르타의 창", "스파르타 전사들이 사용했던 전설의 창입니다.", 7, 0, 3000),
            };
        }

        public void ShopMenu()
        {
            Player player = new Player();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine($"[보유 골드]\n {player.Gold} G\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < shopItems.Count; i++)
                {
                    var item = shopItems[i];
                    string purchaseStatus = item.Purchase ? "구매완료" : $"{item.Gold} G";
                    Console.WriteLine($"- {i + 1} {item.ItemName} | " +
                                      $"{(item.Attack > 0 ? $"공격력 +{item.Attack}" : "")}" +
                                      $"{(item.Defense > 0 ? $" 방어력 +{item.Defense}" : "")} | {item.ItemInfo} | {purchaseStatus}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요: ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.WriteLine("상점에서 나갑니다.");
                    Console.ReadLine();
                    break;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= shopItems.Count)
                {
                    HandlePurchase(choice - 1);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadLine();
                }
            }
        }

        private void HandlePurchase(int index)
        {
            Player player = new Player();
            Inventory inventory = new Inventory();

            var selectedItem = shopItems[index];

            if (selectedItem.Purchase)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (player.Gold >= selectedItem.Gold)
            {
                player.Gold -= selectedItem.Gold;
                selectedItem.Purchase = true;
                inventory.AddItem(selectedItem);
                Console.WriteLine($"구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }

            Console.WriteLine("\n계속하려면 Enter를 누르세요.");
            Console.ReadLine();
        }
    }

    // 메인 시작
    static void MainScene()
    {
        Console.WriteLine("스파르타 마을에 오신 여러분을 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>\n");
    }


    static void Main(string[] args)
    {
        Player player = new Player();
        Inventory inventory = new Inventory();
        Shop shop = new Shop();


        while (true)
        {
            MainScene();

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player.DisplayInfo();
                    break;

                case "2":
                    inventory.InventoryInfo();
                    break;

                case "3":
                    shop.ShopMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                    break;
            }
        }
    }
}
