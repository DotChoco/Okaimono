package com.chocoretostonus.okaimono

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.tooling.preview.Preview
import java.io.File

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            Preview()
        }
    }
}


@Composable
fun App() {
    Column (
        modifier = Modifier
        .fillMaxSize()
        .background(Color.White)
    ){
        Text(text = "Hello World!")
    }
}


@Preview(showBackground = true)
@Composable
fun Preview() {
    App()
}

