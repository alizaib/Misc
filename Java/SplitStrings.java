/* Q11. How to Split String in java? */
public class SplitStrings {
    public static void main(String[] args) {
        String str = "apple,banana,cherry";
        String[] fruits = str.split(",");
        
        for (String fruit : fruits) {
            System.out.println(fruit);
        }
    }
}