package com.chocoretostonus.okaimono.data.notes

import com.chocoretostonus.okaimono.data.notes.local.Note_DAO
import com.chocoretostonus.okaimono.data.notes.model.NoteEntity
import javax.inject.Inject

class NoteRepository @Inject constructor(private val noteDao: Note_DAO){

    fun getAllNotes(): List<NoteEntity>{
        return noteDao.getAll()
    }

    fun insert(noteEntity: NoteEntity){
        noteDao.insert(noteEntity = noteEntity)
    }

    fun update(noteEntity: NoteEntity){
        noteDao.update(noteEntity = noteEntity)
    }

    fun delete(noteEntity: NoteEntity){
        noteDao.delete(noteEntity = noteEntity)
    }

}