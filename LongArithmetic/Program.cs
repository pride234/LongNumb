using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LongArithmetic {


//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	public class PerformanceTimer : IDisposable {
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public Stopwatch _stopwatch;
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public PerformanceTimer(){
			_stopwatch = new Stopwatch();
			_stopwatch.Start(); 
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public void Dispose() {
			Console.WriteLine(_stopwatch.ElapsedMilliseconds);
			_stopwatch.Stop();
		}
	}
//----------------------ProgramClass--------------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------ProgramClass--------------------------------------------------------------------------------------------------------------------------------------------------|


	class Program {
			
		static void Main(string[] args) {

			//BigInteger a = new BigInteger ("ADAFAEADAFAFDFAEFAFDFADFFDBCBCBCBCBFADFFAEFDFAFEFADBCBACBAFDAADADFADADEA23243234142342341241231312312FFEFDAADAFADAEADAFADEADFADAEADFADAEAFFADAEADFADAE");
			//BigInteger b = new BigInteger ("ABDBADBCBACBABCBAFDFADFDAFEFEFAFDFADFACBABCBAADAFADAEADFAEADAECBCBBACBDABEBABCBACBADBBEABBACBABDBDABEBABCBACBAFADFAFEFADFAFDFAFEFDAFAFDCBCBCBCBDBBEBADBA");
			//BigInteger c = new BigInteger ("ACBACBBADFDAFFEFAEFDFAABDBDBACBA12312312312341658948465165468451ADFADFAEFFADFADFFFCFACFFFDFAEFFDFADBBDBEBEBAEBDBABD");
			BigInteger a = new BigInteger("D2524B5718D22699A3A4F53D6330A1AFEA1C07596666281F1E2B9F1553A7D11CD169B3E494F5287C787B9E55EE9D14FD7F395BE16A353C9EBE52516792532FC0EE4C034A47735E3E34467C14179C77FF76A91F7105401B3CDC626D9449CB3635B1831283990833D054EA63EE752525BD020CA59F92C9928140018E7530A365BA");//FE590B8EFCE4C54C436E60B8CCE133806B35E54808216533ADD58AFA0219470E0D1A262C0320755F45CA7FE92EB202CDAB5AF1BBEA44D4B2022F0CE761D27EC3297F4902F986C2C0349FDE82A3BF1F818491DB9B18929BF5CEA0359B29B6FC34BFBA49623AF56D01C177DE1E5CEB3DCAE450F8AAFB5792D36D6AB");
			BigInteger b = new BigInteger("7A5507F8A5E64195F5A4199E");
			//BigInteger c = new BigInteger("D53621DF4872C0773988508C484157D62D8B");

			//using(new PerformanceTimer()) {

			//	a.LongDivision(b);


			//}

			//using(new PerformanceTimer()) {


			//	for(int i = 0;i<10;i++) a.LongDivision(b);

			//}

			//using(new PerformanceTimer()) {

			//	a.DivideBy(b);

			//}

			//using(new PerformanceTimer()) {

			//	for(int i = 0;i<10;i++) a.DivideBy(b);

			//}


			//Console.WriteLine(a.ToHex());
			////Console.WriteLine(a.Deduct(b).Comprassion(c));
			Console.WriteLine(a.DivideBy(b)[0].ToHex());

			Console.ReadKey();
		}
	}


//----------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------------|

//---------------------------Lab1-----------------------------------------------------------------------------------------------------------------------------------------------------|
//---------------------------Lab1-----------------------------------------------------------------------------------------------------------------------------------------------------|

	public class BigInteger {

		public ulong[] number;
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private static ulong[] Padding (ulong[] mas, int len) {
		
			ulong[] padded = new ulong[len];

			for (int i = 0; i<mas.Length; i++) padded[i] = mas[i];
			return padded;
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
		
		private BigInteger ToShiftDigitsToRight (ulong[] array, int i){
			
			BigInteger a = new BigInteger();	

			a.number = new ulong[array.Length + i];

			for (int j = 0; j < array.Length; j++){
				a.number[j+i] = array[j];
			}
			
			return a;
		}		
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger (){
		
			number = new ulong[1];
		}
		
		public BigInteger (ulong[] array){
		
			number = array;
		}

		public BigInteger Clone(BigInteger destination) {
			Array.Resize(ref destination.number, this.number.Length);
			Array.Copy(this.number, destination.number, this.number.Length);
			return destination;
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
	
		public string ToHex() {
			
			string result = "";
			result += number[number.Length-1].ToString("X");
			for (int i = number.Length-2; i >= 0; i--) result += number[i].ToString("X8");
			return result;
		}	
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger HexNotion(string hex) {
			
			BigInteger A = new BigInteger();

			A.number = new ulong[hex.Length];

			for(int i = hex.Length-1; i >= 0;i--) A.number[hex.Length-1-i] = Convert.ToUInt64(hex.Substring(i, 1), 16);
			return A;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private static BigInteger BinaryNotion(BigInteger hex) {
		
			BigInteger res = new BigInteger();
			
			string binary = "";
			binary += Convert.ToString((long)hex.number[hex.number.Length-1],2);
			for(int i = hex.number.Length-2;i >= 0;i--) binary += Convert.ToString((long)hex.number[i],2).PadLeft(32,'0');

			int dig = binary.Length;
			res.number = new ulong[dig];

			for(int i = 0;i<dig;i++) res.number[i] = Convert.ToUInt64(binary.Substring(binary.Length - (i+1),1),2);

			return res;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public static BigInteger BackToHex(BigInteger bin) {

			BigInteger res = new BigInteger();
		
			string binary = "";
			
			for(int j = bin.number.Length-1; j >= 0; j--) binary += bin.number[j].ToString();

			int dig = binary.Length/32;
			if(binary.Length%32 != 0) dig += 1;
			res.number = new ulong[dig];
			int i = 0;

			for(;i < dig-1;i++) res.number[i] = Convert.ToUInt64(binary.Substring(binary.Length - (32*(i+1)),32),2);
			if(32*i < binary.Length) res.number[i] = Convert.ToUInt64(binary.Substring(0,binary.Length - (32*i)),2);

			return res;
		}
////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
//		public string ToBinary() {
			
//			string result = "";
//			for (int i = number.Length-1; i >= 0; i--) result += number[i].ToString();
//			return result;
//		}	
////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
//		public BigInteger BinaryNotion (string binary) {
		
//			BigInteger res = new BigInteger();
		
//			int dig = binary.Length;
//			res.number = new ulong[dig];
			
//			for (int i = 0; i<dig; i++) res.number[i] = Convert.ToUInt64(binary.Substring(binary.Length - (i+1), 1), 2); 

//			return res;
//		}
////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

//		public string FromHexToBinary() {
			
//			ulong[]	 array = new ulong[1]; 
//			array = number;

//			string result = "";
//			result += Convert.ToString((long)array[array.Length-1], 2);
//			for (int i = array.Length-2; i >= 0; i--) result += Convert.ToString((long)array[i], 2).PadLeft(32,'0');
//			return result;
//		}	
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
			long borrow = 0;
			long temp = 0;
			
			for (int i = 0; i<maxlen; i++){
				temp = Convert.ToInt64(a[i]) - Convert.ToInt64(b[i]) - borrow;
				if (temp >= 0) {array[i] = Convert.ToUInt64(temp); borrow = 0;}
				else {array[i] = Convert.ToUInt64(0xFFFFFFFF + temp + 1); borrow = 1;}
			}

			return Humanresult(array, maxlen);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger Multiply (BigInteger num) {
						
			BigInteger temp = new BigInteger();
			BigInteger result = new BigInteger();
		
			for (int i = 0; i<num.number.Length; i++){
				temp = AuxiliaryMult(this, num.number[i]);
				temp = ToShiftDigitsToRight(temp.number, i);
				result = result.Add(temp);
			}

			return result;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger[] DivideBy(BigInteger num) {

			BigInteger[] zero = new BigInteger[1];
			zero[0] = new BigInteger();
			zero[0].number = Padding(zero[0].number, num.number.Length);
			if (num.Comprassion(zero[0]) == 0) {
				Console.WriteLine("Exeption! Division by zero!");
				return zero;
			}
			
			BigInteger R = new BigInteger();
			BigInteger Q = new BigInteger();
			BigInteger C = new BigInteger();
			BigInteger BinNum = new BigInteger();

			BinNum = BinaryNotion(num);

			
			Q.number = Padding(Q.number, BinaryNotion(this).number.Length);
			R = this;
			
			int k = BinNum.number.Length;
			int t;
			
			while(R.Comprassion(num) != -1){
				R = BinaryNotion(R);
				t = R.number.Length;
				C = ToShiftDigitsToRight(BinNum.number, t-k);
				if (R.Comprassion(C) == -1){
					t -= 1;
					C = ToShiftDigitsToRight(BinNum.number, t-k);
				}
				
				R = BackToHex(R);
				C = BackToHex(C);
				R = R.Deduct(C);
				Q.number[t-k] = 1;
			}
			
			BigInteger[] D = new BigInteger[2];
			Q = BackToHex(Q);
			D[0] = Humanresult(Q.number, Q.number.Length);
			D[1] = Humanresult(R.number, R.number.Length);
			return D;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger GoGornerPower(BigInteger num) {
		
			BigInteger C = new BigInteger("1");
			BigInteger zero = new BigInteger();
			zero.number = Padding(zero.number, num.number.Length);
			
			if (num.Comprassion(zero) == 0) {
				return C;
			}
			
			BigInteger[] D = new BigInteger[16];
		 
			D[0] = C; 
			D[1] = this;
			
			for (int i = 2; i < 16; i++) D[i] = D[i-1].Multiply(this);
				
			BigInteger Temp = new BigInteger();
			Temp = num.HexNotion(num.ToHex());
			
			for (int i = num.HexNotion(num.ToHex()).number.Length - 1; i>=0; i --) {
				
				C = C.Multiply(D[Temp.number[i]]);	
				
				if (i != 0) {
					for (int j = 0; j < 4; j++) C = C.Multiply(C);
				}
			}
			
			return Humanresult(C.number, C.number.Length);
		}
//---------------------------Lab2-----------------------------------------------------------------------------------------------------------------------------------------------------|
//---------------------------Lab2-----------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger DivByTwo () {
			
			BigInteger Temp = new BigInteger();
			BigInteger Res = this;
			ulong temp = 0;
			//Res.number[0] = Res.number[0] >> 1;
			
			for (int i = 0; i<Res.number.Length; i++){
				if ((Res.number[i] & 1) == 0) {
					Res.number[i] >>= 1;
					continue;
				}
				temp = (Res.number[i] << 31) + Res.number[i-1];
				Res.number[i-1] = temp & 0xFFFFFFFF;
				Res.number[i] = temp >> 0x20;
			}
			return Humanresult(Res.number, Res.number.Length);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public static BigInteger[] gcd_lcm (BigInteger a, BigInteger b) {
			
			BigInteger d = a.Euclid(b);

			BigInteger zero = new BigInteger();
			zero.number = Padding(zero.number, b.number.Length);			
			BigInteger[] D = new BigInteger[2];

			if (a.Comprassion(zero) == 0 || b.Comprassion(zero) == 0) {	
				//Console.WriteLine("\nA GCD is: " + d.ToHex() + "\n\nA LCM is: 0"); 
				D[0] = d;
				D[1] = zero;				
			} else {		
				//Console.WriteLine("\nA GCD is: " + d.ToHex() + "\n\nA LCM is: " + a.Multiply(b).DivideBy(d)[0].ToHex());				
				D[0] = d;
				D[1] = a.Multiply(b).DivideBy(d)[0];
			} 			
			return D;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger Euclid (BigInteger B){
			
			BigInteger a = this;
			BigInteger b = B;

		
			BigInteger zero = new BigInteger();
			zero.number = Padding(zero.number, b.number.Length);
			
			if (a.Comprassion(zero) == 0) return b;
			if (b.Comprassion(zero) == 0) return a;

			BigInteger d = new BigInteger("1");
			BigInteger temp = new BigInteger();

			while (((a.number[0] & 1) == 0) && ((b.number[0] & 1) == 0)) {
				a = a.DivByTwo();
				b = b.DivByTwo();
				d = d.AuxiliaryMult(d, 2);
			}

			while ((a.number[0] & 1) == 0) a = a.DivByTwo();				
			
			while (b.Comprassion(zero) != 0) {
				
				while ((b.number[0] & 1) == 0) b = b.DivByTwo();

				if (a.Comprassion(b) != -1){
					temp = a;
					a = b;
					b = temp.Deduct(b);
				}

				else b = b.Deduct(a);
			}

			d = d.Multiply(a);
			return d;
		} 
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger TO_KILL (int k){
			
			if (k==0) return this;
			if (this.number.Length <= k) {BigInteger zero = new BigInteger(); return zero;}

			ulong[] array = new ulong[this.number.Length-k];

			for (int i = 0; i < array.Length; i++) 
				array[i] = this.number[i+k];

			BigInteger res = new BigInteger(array);

			return res;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger mod(BigInteger n){
			
			BigInteger zero = new BigInteger();
			zero.number = Padding(zero.number, n.number.Length);
					
			if (n.Comprassion(zero) == 0) {
				throw new DivideByZeroException();
			}

			return this.DivideBy(n)[1];
		
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger Barrett(BigInteger n, BigInteger miu) {
		
			BigInteger q = new BigInteger();
			BigInteger r = new BigInteger();
			
			q = this.TO_KILL(n.number.Length-1);
			q = q.Multiply(miu);
			q = q.TO_KILL(n.number.Length+1);
			r = this.Deduct(q.Multiply(n));
			
			while (r.Comprassion(n)!=-1) r = r.Deduct(n);

			return r;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger GoGornerPowerByBarrett(BigInteger num, BigInteger n) {
		
			BigInteger zero = new BigInteger();
			zero.number = Padding(zero.number, n.number.Length);
			if (n.Comprassion(zero) == 0) {Console.WriteLine("Exeption! Division by zero!"); return zero;}
			
			BigInteger C = new BigInteger("1");
			
			if (num.Comprassion(zero) == 0) return C;

			BigInteger A = new BigInteger();			
			BigInteger B = new BigInteger();
			BigInteger miu = new BigInteger("100000000");
			A = this;
			B = BinaryNotion(num);

			if (n.number.Length == 1) miu = ToShiftDigitsToRight(miu.number, 1);
			else miu = ToShiftDigitsToRight(miu.number, 2*n.number.Length-1);
			
			miu = miu.DivideBy(n)[0];
			A = A.mod(n);


			for (int i = 0; i<B.number.Length; i++) {

				if (B.number[i] == 1) C = C.Multiply(A).Barrett(n, miu);
				A = A.Multiply(A).Barrett(n, miu);

			}
			return Humanresult(C.number, C.number.Length);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public BigInteger[] LongDivision (BigInteger num) {

			BigInteger[] zero = new BigInteger[1];
			zero[0] = new BigInteger();
			zero[0].number = Padding(zero[0].number, num.number.Length);
			if (num.Comprassion(zero[0]) == 0) {
				Console.WriteLine("Exeption! Division by zero!");
				return zero;
			}
			BigInteger A = this;
			BigInteger R = new BigInteger();
			BigInteger Q = new BigInteger();
			BigInteger C = new BigInteger();
			BigInteger One = new BigInteger("1");
			BigInteger Temp = new BigInteger();
			int k = 0;

			while (A.Comprassion(num) != -1) {
				
				Temp = ToShiftDigitsToRight(num.number, A.number.Length - num.number.Length);
				
				if (A.Comprassion(Temp) != -1) {
					Temp = zero[0];
					Temp.number = Padding(Temp.number, num.number.Length);
				}
				else {
					Temp = zero[0];
					Temp.number = Padding(Temp.number, num.number.Length+1);
				}	
				
				for (int i = 0; i<Temp.number.Length; i++) Temp.number[Temp.number.Length - 1 - i] = A.number[A.number.Length - 1 - i];
				
				k = Temp.number.Length;

				while (Temp.Comprassion(num) != -1) {
					Temp = Temp.Deduct(num);
					C = C.Add(One);					
				}
			
				C = ToShiftDigitsToRight(C.number, A.number.Length - k);
				
				if (Temp.Comprassion(zero[0]) != 0) {
					Temp = ToShiftDigitsToRight(Temp.number, A.number.Length - k);
					for (int i = 0; i<A.number.Length - k; i++) Temp.number[i] = A.number[i];
					A = Temp;
				}			
				else {
					Temp.number = Padding(Temp.number, A.number.Length - k);
					for (int i = 0; i<A.number.Length - k; i++) Temp.number[i] = A.number[i];
					A = Temp;
				}

				Q = Q.Add(C);
				C = zero[0];
				Temp = zero[0];
			}

			BigInteger[] D = new BigInteger[2];
			D[0] = Q;
			D[1] = A;			
			return D;
		}
//----------------------EndOfBigIntegerClass------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------EndOfBigIntegerClass------------------------------------------------------------------------------------------------------------------------------------------|
	}
//----------------------EndOfnamespase------------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------EndOfnamespase------------------------------------------------------------------------------------------------------------------------------------------------|
}
