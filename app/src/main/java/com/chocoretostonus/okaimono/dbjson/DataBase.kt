package com.chocoretostonus.okaimono.dbjson

import java.util.Date


class DataBase{
    var Anime:ArrayList<Anime> = arrayListOf(Anime()) as ArrayList<Anime>
    var Manga:ArrayList<Manga> = arrayListOf(Manga()) as ArrayList<Manga>
    var LastUpdate:String = Date().toString()
}

class Anime {
    var Name:String = "Unknown"
    var Tags: ArrayList<String> = arrayListOf("None")
    var InLive:Boolean = false
    var NextNewCap:String = "Never"
    var MaxCaps:Int = 0
    var LastViewCap:Int = 0
    var Prequels: ArrayList<String>? = arrayListOf("None")
    var Sequels: ArrayList<String>? = arrayListOf("None")
    var Movies: ArrayList<String>? = arrayListOf("None")
    var SpinOffs: ArrayList<String>? = arrayListOf("None")
    var Ovas: Int = 0
}


class Manga {
    var Name:String = "Unknown"
    var Tags: ArrayList<String> = arrayListOf("None")
    var OnGoing:Boolean = false
    var MaxCaps:Int = 0
    var LastViewCap:Int = 0
    var Prequels: ArrayList<String> = arrayListOf("None")
    var Sequels: ArrayList<String> = arrayListOf("None")
    var SpinOffs: ArrayList<String> = arrayListOf("None")
}

