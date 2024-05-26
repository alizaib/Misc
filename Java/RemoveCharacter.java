/* Q16. Write a method that will remove given character from the String? */
public class RemoveCharacter {
    public static void main(String[] args) {
        String originalString = "Hello World";
        char charToRemove = 'o';

        String resultString = removeCharacter(originalString, charToRemove);
        System.out.println("Original String: " + originalString);
        System.out.println("Character to Remove: " + charToRemove);
        System.out.println("Modified String: " + resultString);
    }

    // Method to remove all occurrences of a given character from a string
    public static String removeCharacter(String str, char ch) {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < str.length(); i++) {
            if (str.charAt(i) != ch) {
                result.append(str.charAt(i));
            }
        }

        return result.toString();
    }
}
