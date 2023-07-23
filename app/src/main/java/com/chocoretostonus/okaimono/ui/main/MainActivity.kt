package com.chocoretostonus.okaimono.ui.main

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.tooling.preview.Preview
import com.chocoretostonus.okaimono.ui.main.views.HomeScreen
import com.chocoretostonus.okaimono.ui.theme.OkaimonoTheme

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
    OkaimonoTheme(){
        HomeScreen()
    }
}


@Preview()
@Composable
fun Preview() {
    App()
}

