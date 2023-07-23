package com.chocoretostonus.okaimono.dbjson
import com.google.gson.Gson
import java.io.FileReader
import java.io.FileWriter
import java.util.Date

//main class
var DB = DataBase()
val gson = Gson()

fun main(){
//    ReadFile()

}


fun CreateDeleteFile(){
    val writer = FileWriter("DOKDB.json")
    var Json = String()
    DB = DataBase()

    Json = gson.toJson(DB)
    writer.write(Json)
    writer.close()
}


fun ReadFile(){

    //text plane
    var JsonFile = String()
    try {
        //json path file
        val reader = FileReader("DOKDB.json")

        reader.readLines().forEach() { JsonFile += it }

        //obtain data of json
        DB = gson.fromJson(JsonFile, DataBase::class.java)
        println(JsonFile)
    }
    catch (e: Exception){
        println("Error: $e")
        CreateDeleteFile()
    }

}


fun UpdateFile(TODB: Any){

    val writer = FileWriter("DOKDB.json")
    var data = DataBase()
    var json = String()

    if (TODB is Anime){
        if (data.Anime != null){
            data.Anime!!.add(TODB)
        }
        else{
            data.Anime = arrayListOf(TODB as Anime)
        }
    }
    else if (TODB is Manga){
        if (data.Manga != null){
            data.Manga!!.add(TODB)
        }
        else{
            data.Manga = arrayListOf(TODB as Manga)
        }
    }
    data.LastUpdate = Date().toString()

    json = gson.toJson(data)
    writer.write(json)
    writer.close()
}



