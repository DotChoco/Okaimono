package com.chocoretostonus.okaimono.data.notes.local

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.Query
import androidx.room.Update
import com.chocoretostonus.okaimono.data.notes.model.NoteEntity
import com.chocoretostonus.okaimono.utlis.Constants

@Dao
interface Note_DAO{
    @Query("SELECT * FROM ${Constants.note_table_name}")
    fun getAll(): List<NoteEntity>


    @Insert
    fun insert(noteEntity: NoteEntity)


    @Delete
    fun delete(noteEntity: NoteEntity)


    @Update
    fun update(noteEntity: NoteEntity)
}
