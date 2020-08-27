use std::{fs, path::PathBuf};

// File guard to ensure test file cleanup
#[allow(dead_code)]
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

#[allow(dead_code)]
impl TestFileGuard {
    pub fn new(file_path: &str) -> TestFileGuard {
        TestFileGuard {
            file_path: PathBuf::from(&file_path),
        }
    }
}
