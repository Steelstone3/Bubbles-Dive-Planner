use inquire::{Confirm, Text};

pub fn text_prompt(message: &str, help_prompt: &str, default_value: &str) -> String {
    Text::new(message)
        .with_help_message(help_prompt)
        .with_default(default_value)
        .prompt()
        .unwrap()
}

pub fn confirmation(message: &str) -> bool {
    Confirm::new(message)
        .with_default(false)
        .prompt()
        .unwrap_or_default()
}

pub fn parse_numeric_value(input: String) -> u32 {
    match input.chars().find(|character| character.is_numeric()) {
        Some(_) => input.as_str().trim().parse::<u32>().unwrap(),
        None => panic!("Not a numeric value"),
    }
}
