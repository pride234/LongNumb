using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongArithmetic {


//-----------------------ProgramClass-------------------------------------------------------------------------------------------------------------------------------------------------|
//-----------------------ProgramClass-------------------------------------------------------------------------------------------------------------------------------------------------|


	class Program {

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
			
		static void Main(string[] args) {

		BigInteger a = new BigInteger ("FFFFDDAADADF");
		BigInteger b = new BigInteger ("51AADAADF"); 
		Console.WriteLine(a.Sub(b).ToHex());
		Console.ReadKey();
		}
	}


//-----------------------BigIntegerClass----------------------------------------------------------------------------------------------------------------------------------------------|
//-----------------------BigIntegerClass----------------------------------------------------------------------------------------------------------------------------------------------|


	class BigInteger {

		private ulong[] number;
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
		
		public BigInteger (){
		
			number = new ulong[1];
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger (string hex){
			
			int dig = hex.Length/8;
			if (hex.Length%8 != 0) dig += 1;
			number = new ulong[dig];
			int i = 0;

			for (; i < dig-1; i++) number[i] = Convert.ToUInt64(hex.Substring(hex.Length - (8*(i+1)), 8), 16);
			if (8*i < hex.Length)  number[i] = Convert.ToUInt64(hex.Substring(0, hex.Length - (8*i)), 16);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private static ulong[] Padding (ulong[] mas, int len) {
		
			ulong[] pad = new ulong[len];

			for (int i = 0; i<mas.Length; i++) pad[i] = mas[i];
			return pad;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public ulong[] Comp(ulong[] a, ulong[] b){
			
			int i = a.Length - 1;
			if (a[i] > b[i]) return a;
			
			while (a[i] == b[i]) {
				i -= 1;
				if (i == -1) return a;
				else if (a[i] > b[i]) return a;				
			}
			
			return b;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public string ToHex() {
			
			string result = "";
			result += number[number.Length-1].ToString("X");
			for (int i = number.Length-2; i >= 0; i--) result += number[i].ToString("X").PadLeft(8, '0');
			return result;
		}	
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public BigInteger Add(BigInteger num) {
			
			int maxlen = 1 + (this.number.Length >= num.number.Length ? this.number.Length : num.number.Length);
			ulong[] a = Padding(this.number, maxlen);
			ulong[] b = Padding(num.number, maxlen);
			ulong[] array = new ulong[maxlen];
			ulong carry = 0, temp;

			for (int i = 0; i<maxlen; i++){
				temp = a[i] + b[i] + carry;
				array[i] = temp & 0xFFFFFFFF;
				carry = temp >> 0x20;
			}
		
			BigInteger result = new BigInteger();
			if (array[maxlen-1] != 0){
				result.number = array;
				return result;
			}
			else {
				ulong[] humanresult = new ulong[array.Length-1];
				for (int i = 0; i<humanresult.Length; i++) humanresult[i] = array[i];
				result.number = humanresult;
				return result;			
			} 
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger Sub(BigInteger num) {
			int maxlen =  this.number.Length >= num.number.Length ? this.number.Length : num.number.Length;
			ulong[] a = Padding(this.number, maxlen);
			ulong[] b = Padding(num.number, maxlen);
			ulong[] array = new ulong[maxlen];
			if (Comp(a, b) == b) {
				Console.WriteLine("Answer is a negative number! The secong operand is bigger that the first. Please, correct this. Look at these: \n");
				Console.WriteLine(this.ToHex());
				return num;
			}
			ulong borrow = 0;
			
			for (int i = 0; i<maxlen; i++){
				if ((a[i] - borrow) < b[i]) {
					array[i] = a[i] - b[i] - borrow + 0xFFFFFFFF + 1;
					borrow = 1;
				}
				else {
					array[i] = a[i] - b[i] - borrow;
					borrow = 0;
				}
				//temp = a[i] - b[i] - borrow;
				//if (temp >=0) {
				//	array[i] = temp;
				//	borrow = 0;
				//}
				//else {
				//	array[i] = temp + 0xFFFFFFFF + 1;
				//	borrow = 1;
				//}
			}

			BigInteger result = new BigInteger();
			if (array[maxlen-1] != 0){
				result.number = array;
				return result;
			}
			else {
				ulong[] humanresult = new ulong[array.Length-1];
				for (int i = 0; i<humanresult.Length; i++) humanresult[i] = array[i];
				result.number = humanresult;
				return result;			
			} 
		}
	}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
}
