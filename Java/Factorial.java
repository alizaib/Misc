import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Scanner;

// The main method must be in a class named "Main".
public class Factorial {
    public static void main(String[] args) {
		Scanner in = new Scanner(System.in);
		System.out.println("Enter the number you want to find factorial for: ");
		int a = in.nextInt();
		
		int result = 1;
		for(int i=1; i<=a; i++) {
			result = result * i;
		}
		System.out.println("factorial of " + a + " is " + result);
    }
}