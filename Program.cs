using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2{
//--------------------------------------------ProgramClass-------------------------------------------------------------------------------------------------------------------------------------------|        
//--------------------------------------------ProgramClass-------------------------------------------------------------------------------------------------------------------------------------------|        


    class Program{
        static void Main(string[] args) {
            //BigInteger a = new BigInteger("FFFFFFFFFFFFFFFFFFFFAAAAAAAAAAAAAADDDDDDDDDDDDDDDD");
            //Console.WriteLine(ulong.MaxValue > ulong.MaxValue);
            BigInteger a = new BigInteger("8C78744E2F49DF62D13AD204E00F731BAE0E085C353D8D758E06E4DFFB37B57A66ECC52CF2D7D888C49C2794E6FB944C4183A128203932FEBEA4B6E62B2EBDAD");
            BigInteger b = new BigInteger("4112652E8135D145329F0DAE738F75C35004A154F1C43449DB87B6BE0F3EBF5B3BA1016F0A04A10C7EA76C3D30EEDB34B1E6E1009B3FF5C987FA313097485E61");
            Console.WriteLine(a.Add(b).ToHex());
            Console.ReadKey();
        }
    }
//-------------------------------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------|        
//-------------------------------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------|        


    class BigInteger {

        private ulong [] number;
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

        public BigInteger() {

            number = new ulong[1];
        } 
 //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

        public BigInteger(string hex){

            int numbD = hex.Length / 8; 
            if (hex.Length%8 != 0) numbD += 1;
            number = new ulong[numbD];
            int i = 0;

            for(;i < numbD-1;i++) {
                number[numbD-i-1] = (ulong)Convert.ToInt64(hex.Substring(i*8,8), 16);
            }

            if(i < hex.Length) number[0] = (ulong)Convert.ToInt64(hex.Substring(i*8,hex.Length-(i*8)), 16);
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

        public string ToHex() {

            string result = "";

            for (int i = 0; i<number.Length; i++) {
                result += number[number.Length-i-1].ToString("X").PadLeft(8, '0');             
            }

            return result;
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

        public BigInteger Add(BigInteger num) {

            int maxlen = 1 + (this.number.Length >= num.number.Length ? this.number.Length : num.number.Length);
            ulong[] a = Padding(this.number, maxlen);
            ulong[] b = Padding(num.number, maxlen);
            ulong carry = 0, temp;
            ulong[] array = new ulong[maxlen];

            for(int i=0;i<maxlen;i++) {
                temp = a[i] + b[i] + carry;
                array[i] = temp & 0xFFFFFFFF;
                carry = temp >> 0x20;
            }

            BigInteger result = new BigInteger();
            if (array[maxlen-1] != 0) {
                result.number = array;
                return result;
            } 
            else {
               ulong[] humanresult = new ulong[array.Length-1];			
               for (int i = 0; i<humanresult.Length;i++) humanresult[i] = array[i];
               result.number = humanresult;
               return result;
            }
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

        public static ulong[] Padding (ulong[] num, int n) {

            ulong[] result = new ulong[n];
            for (int i = 0; i<num.Length; i++) result[i] = num[i];
            return result;
        }
    }   
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

	public BigInteger Compare (BigInteger num){
		
		
		return 0;
	}
}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        



//--------------------------Свалка-------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

/*class Animal
 {
     public virtual void Run() {
         Console.Write("Animal is running! A ");
     }

 }
 //----------------------------------------------------------------------------------------------------------

 class Dog : Animal{
     public override void Run()
     {
         base.Run();
         Console.WriteLine("Dog's running!");
     }
 }
 class Cat: Animal
 {
     public override void Run()
     {
         base.Run();
         Console.WriteLine("Cat's running!");

     }
     public override string ToString()
     {
         return "something";
     }
 }*/ //классы, перегрузка,  наследование
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

/*     Animal doggie = new Dog();
        Animal cat = new Cat();
        cat.Run();
        doggie.Run();
        Console.WriteLine(cat.ToString());*/ //вызовы методов класса
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

/*          Console.WriteLine("Please enter a number:");
            //var number1 = long.Parse(Console.ReadLine()); //преобразование вводных данных в long;
            Console.WriteLine(long.Parse(Console.ReadLine())); // раньше тут было в скобках number1;
            Console.ReadKey();*/ //преобразование вводных данных
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|   

/*            set("hell", "hell2", 4564123456, 123212345678, 6578954);
                                            Console.ReadKey();
                                        }
                                        static void set(params string[] name, params ulong[] ulongnumb) { //метод может принимать на вход либо один массив, либо один массив и ещё другие параметры, этот массив неправильный
                                            foreach (string numb in name) 
                                                Console.WriteLine("Number is: " + numb);
                                       }*/ //массивы с мусором и просто массивы
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|    

//можно спользовать "//TODO:   ", "//NOTE:   ", "//UNDONE:   " и "//HACK:   " для списка задач в вижуалке
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|        

/*          var n = int.Parse(Console.ReadLine());
            if(( n > 10) || ( n < 0))
                Console.WriteLine("Hey! The number should be 0 or more and 10 or less!");
            else
                Console.WriteLine("Good job!");
            Console.ReadKey();*/ //the if statement
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

/*int number = 2;
switch(number) {
case 0:
    Console.WriteLine("The number is zero!");
    break;
case 1:
    Console.WriteLine("The number is one!");
    break;
case 2:
    Console.WriteLine("The number is not one or zero! It's two!");
    break;
}*/

/*            Console.WriteLine("Do you enjoy C# ? (yes/no/maybe)");
            string input = Console.ReadLine();
            switch(input.ToLower()) { //преобразуем входящие данные в нижний регистр
                case "yes":
                case "maybe":
                    Console.WriteLine("Great!");
                    break;
                case "no":
                    Console.WriteLine("Too bad!");
                    break;
                default:
                    Console.WriteLine("I'm sorry, I don't understand that!");
                    break;
            }*/ //the switch statements
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

/*float number = 1;
ulong grad = 10;
            while(number< 2) {
                Console.WriteLine(number);
                number = number + (1/grad);
                grad = grad*10;
            }*/ // the while loop и моя попытка создать бесконечный цикл 

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

    /*           Car car;
                           //car = new Car("Red");
                           //Console.WriteLine(car.Describe());
                           car = new Car("NotRed");
                           Console.WriteLine(car.Describe());
                           Console.ReadKey();
                       }
                   }
                   class Car {
                       private string color;

                       public string Describe() {
                           return "This car is " + Color;
                       }

                       public string Color {
                           get {
                               return color;
                           }
                           set {                                               //наебалово, оно в set даже не заходит, на сайте наёбка
                               if(value == "Red")
                                   color = value;
                               else
                                   Console.WriteLine("This car can only be red!");
                           }
                       }
                       public Car(string color) {
                           this.color = color;
                       }
                   }*/
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
