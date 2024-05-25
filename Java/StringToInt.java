import java.util.HashMap;
import java.util.Scanner;

public class StringToInt {
    public static void main(String[] args) {
		Scanner in = new Scanner(System.in);
		System.out.print("Enter the input string: ");
		String input = in.nextLine();		
		int number = Integer.parseInt(input);
		
		System.out.println("You entered: " + number);
		System.out.println("when multipled by 2: " + number*2);
    }
}