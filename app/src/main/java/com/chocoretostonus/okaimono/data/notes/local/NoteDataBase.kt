package com.chocoretostonus.okaimono.data.notes.local

import androidx.room.Database
import androidx.room.RoomDatabase
import com.chocoretostonus.okaimono.data.notes.model.NoteEntity
import androidx.room.TypeConverters
//import com.chocoretostonus.okaimono.data.notes.Converter

//@TypeConverters(Converter::class)
@Database(entities = [NoteEntity::class], version = 1)
abstract class NoteDataBase : RoomDatabase() {
    abstract fun noteDao(): Note_DAO
}