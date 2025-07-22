public class JavaTest {
    private static final String[] properties = new String[] {
        "os.arch",
        "java.version",
        "java.vendor"
    };

    public static void main(String[] args) {
        for (String key : properties) {
            String property = System.getProperty(key);
            if (property != null) {
                System.out.println(key + "=" + property);
            }
        }
    }
}
