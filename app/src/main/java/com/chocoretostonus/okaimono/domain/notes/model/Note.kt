package com.chocoretostonus.okaimono.domain.notes.model

import androidx.room.ColumnInfo
import com.chocoretostonus.okaimono.data.notes.model.NoteEntity

data class Note(
    val id: Int,
    val Name: String? = "Unknown",
//    val Tags: String? = "None",
    val InLive: Boolean? = false,
    val NextNewCap: String? = "Never",
    val MaxCaps: Int? = 0,
    val LastViewCap: Int? = 0,
    val Prequels: String? ="None",
    val Sequels: String? ="None",
    val Movies: String? ="None",
    val SpinOffs: String? ="None",
    val Ovas: Int? = 0
)



fun NoteEntity.toNote(): Note = Note(
    id = uid,
    Name = Name,
//    Tags = Tags,
    InLive = InLive,
    NextNewCap = NextNewCap,
    MaxCaps = MaxCaps,
    LastViewCap = LastViewCap,
    Prequels = Prequels,
    Sequels = Sequels,
    Movies = Movies,
    SpinOffs = SpinOffs,
    Ovas = Ovas
)


fun Note.toNoteEntity(): NoteEntity = NoteEntity(
    uid = id,
    Name = Name,
//    Tags = Tags,
    InLive = InLive,
    NextNewCap = NextNewCap,
    MaxCaps = MaxCaps,
    LastViewCap = LastViewCap,
    Prequels = Prequels,
    Sequels = Sequels,
    Movies = Movies,
    SpinOffs = SpinOffs,
    Ovas = Ovas
)