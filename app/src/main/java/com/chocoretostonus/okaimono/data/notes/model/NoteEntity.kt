package com.chocoretostonus.okaimono.data.notes.model

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import com.chocoretostonus.okaimono.utlis.Constants


@Entity(tableName = Constants.note_table_name)
data class NoteEntity(
    @PrimaryKey val uid: Int,
    @ColumnInfo(name = "Name") val Name: String? = "Unknown",
//    @ColumnInfo(name = "Tags") val Tags: String? = "None",
    @ColumnInfo(name = "InLive") val InLive: Boolean? = false,
    @ColumnInfo(name = "NextNewCap") val NextNewCap: String? = "Never",
    @ColumnInfo(name = "MaxCaps") val MaxCaps: Int? = 0,
    @ColumnInfo(name = "LastViewCap") val LastViewCap: Int? = 0,
    @ColumnInfo(name = "Prequels") val Prequels: String? ="None",
    @ColumnInfo(name = "Sequels") val Sequels: String? ="None",
    @ColumnInfo(name = "Movies") val Movies: String? ="None",
    @ColumnInfo(name = "SpinOffs") val SpinOffs: String? ="None",
    @ColumnInfo(name = "Ovas") val Ovas: Int? = 0
)