use arboard::Clipboard;
use crossterm::event::Event::Key;
use crossterm::event::{DisableMouseCapture, EnableMouseCapture, KeyCode};
use crossterm::terminal::{
    disable_raw_mode, enable_raw_mode, EnterAlternateScreen, LeaveAlternateScreen,
};
use crossterm::{event, execute};
use std::error::Error;
use rusqlite::ErrorCode;
use tui::backend::{Backend, CrosstermBackend};
use tui::layout::{Alignment, Constraint, Direction, Layout, Rect};
use tui::style::{Color, Modifier, Style};
use tui::text::Span;
use tui::widgets::{Block, BorderType, Borders, Clear, List, ListItem, ListState, Paragraph};
use tui::{Frame, Terminal};

const APP_KEYS_DESC: &str = "
L:            List
U:            On List, It's copy the Username
P:            On List, It's copy the Password
D:            On List, It's Delete
E:            On List, It's Edit
S:            Search
Insert Btn:   Insert new Password
Tab:          Go to next field
Shift+Tab:    Go to previous field
Esc:          Exit insert mode
";

enum InputMode {
    Normal,
    Title,
    Username,
    Password,
    Submit,
    Search,
    List,
    Delete,
}

fn main() {


}
