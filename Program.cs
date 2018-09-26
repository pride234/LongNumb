using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongArithmetic {


//----------------------ProgramClass--------------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------ProgramClass--------------------------------------------------------------------------------------------------------------------------------------------------|


	class Program {

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
			
		static void Main(string[] args) {

		BigInteger a = new BigInteger ("AADADADFAFDADFAFDADAFAFADADADFADAD13545151631351315465165641321aDADADADFAFADADAFADA465461351");
		BigInteger b = new BigInteger ("AADADADFAFDADFAFDADAFAFADADADFADAD13545151631351315465165641321aDADADADFAFADADAFADA465461351"); 
		BigInteger c = new BigInteger ("720765CA95A7ECCF2341C0590307E3CA8A1FC1D8A33F96D6C26A536EB02B7D964975C2E224A3297518A97925CEA3E53E126F10A89A77D19296198B934AC75142EC15408B531BA5BD93529A784A44FED557A79FD682B298347BC11FA1");
		Console.WriteLine(a.Multiply(b).ToHex());
		Console.WriteLine(a.Multiply(b).Comprassion(c));
		Console.ReadKey();
		}
	}


//----------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------------|


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

		private static BigInteger Humanresult (ulong[] array, int maxlen){
			
			BigInteger result = new BigInteger();
			int i = maxlen-1;
			
			while (array[i] == 0) {
				i--;
				if (i == -1) return result;
			}

			ulong[] humanresult = new ulong[i+1];
			for (int j = 0; j<humanresult.Length; j++) humanresult[j] = array[j];
			result.number = humanresult;
			return result;			 
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger AuxiliaryMult (BigInteger num, ulong b){
						
			ulong carry = 0, temp;
			ulong[] array = new ulong[num.number.Length+1];

			for (int i = 0; i < num.number.Length; i++) {
				temp = num.number[i]*b + carry;
				array[i] = temp & 0xFFFFFFFF;
				carry = temp >> 0x20;
			}

			array[num.number.Length] = carry;
			return Humanresult(array, num.number.Length+1);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
		
		private ulong[] ShiftRight (ulong[] array, int i){
			
			ulong[] a = new ulong[array.Length+i];
			
			for (int j = 0; j < array.Length; j++){
				a[j+i] = array[j];
			}
			
			return a;
		}


//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public string ToHex() {
			
			string result = "";
			result += number[number.Length-1].ToString("X");
			for (int i = number.Length-2; i >= 0; i--) result += number[i].ToString("X").PadLeft(8, '0');
			return result;
		}	
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public int Comprassion(BigInteger num) {
			int len = this.number.Length >= num.number.Length ? this.number.Length : num.number.Length;
			ulong[] a = Padding(this.number, len);
			ulong[] b = Padding(num.number, len);

			int i = a.Length - 1;
			if (a[i] > b[i]) return 1;
			
			while (a[i] == b[i]) {
				i -= 1;
				if (i == -1) return 0;
				else if (a[i] > b[i]) return 1;				
			}			
			
			return -1;
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
		
			return Humanresult(array, maxlen);	
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger Deduct(BigInteger num) {
			
			if (this.Comprassion(num) == -1) {
				Console.WriteLine("Answer is a negative number! The secong operand is bigger that the first. Please, correct this. Look at them: \n");
				Console.WriteLine(this.ToHex());
				return num;
			}

			int maxlen = this.number.Length >= num.number.Length ? this.number.Length : num.number.Length;
			ulong[] a = Padding(this.number, maxlen);
			ulong[] b = Padding(num.number, maxlen);
			ulong[] array = new ulong[maxlen];
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
			}

			return Humanresult(array, maxlen);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger Multiply (BigInteger num) {
			
			int maxlen = this.number.Length + num.number.Length + 1;
			
			BigInteger temp = new BigInteger();
			BigInteger result = new BigInteger();
		
			for (int i = 0; i<num.number.Length; i++){
				temp = AuxiliaryMult(this, num.number[i]);
				temp.number = ShiftRight(temp.number, i);
				result = result.Add(temp);
			}

			return result;
		}

//----------------------EndOfBigIntegerClass------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------EndOfBigIntegerClass------------------------------------------------------------------------------------------------------------------------------------------|
	}
//----------------------EndOfnamespase------------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------EndOfnamespase------------------------------------------------------------------------------------------------------------------------------------------------|
}
