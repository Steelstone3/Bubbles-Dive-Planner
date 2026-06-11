#[cfg(test)]
pub mod test_file_guard {
    use std::{fs, path::PathBuf};

    // File guard to ensure test file cleanup
    pub struct TestFileGuard {
        pub file_path: PathBuf,
    }

    impl Drop for TestFileGuard {
        fn drop(&mut self) {
            if self.file_path.exists() {
                fs::remove_file(&self.file_path).unwrap_or_default();
            }
        }
    }

    impl TestFileGuard {
        pub fn new(file_path: &str) -> TestFileGuard {
            TestFileGuard {
                file_path: PathBuf::from(&file_path),
            }
        }
    }
}
