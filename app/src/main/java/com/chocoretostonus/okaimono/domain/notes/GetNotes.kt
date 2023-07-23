package com.chocoretostonus.okaimono.domain.notes

import com.chocoretostonus.okaimono.data.notes.NoteRepository
import com.chocoretostonus.okaimono.domain.notes.model.Note
import com.chocoretostonus.okaimono.domain.notes.model.toNote
import javax.inject.Inject

class GetNotes @Inject constructor(private val noteRepository: NoteRepository){
    suspend operator fun invoke(): List<Note>{
        return noteRepository.getAllNotes().map {it.toNote()}
    }
}