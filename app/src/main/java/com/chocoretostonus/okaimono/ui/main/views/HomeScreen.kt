package com.chocoretostonus.okaimono.ui.main.views

import android.R
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.runtime.Composable
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import com.chocoretostonus.okaimono.ui.theme.OkaimonoTheme

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun HomeScreen() {

    Scaffold(
    modifier = Modifier
        .fillMaxSize()
        .background(MaterialTheme.colorScheme.background),
        floatingActionButton = {
            ExtendedFloatingActionButton(onClick = { /*TODO*/ },
                icon = { Icon(Icons.Filled.Add, contentDescription = null) },
                text = { Text("Add") }
        )}
    )

    {
        Column(modifier = Modifier.padding(it)) {

        }

    }
}

@Composable
fun FakeHomeScreen() {
    OkaimonoTheme(){
        HomeScreen()
    }
}


@Preview
@Composable
fun Preview() {
    FakeHomeScreen()
}