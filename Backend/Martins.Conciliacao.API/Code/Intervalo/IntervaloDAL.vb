Imports Oracle.ManagedDataAccess.Client
Imports Martins.Conciliacao.Model
Imports Martins.Conciliacao.Util

Namespace Code.Intervalo
	Public Class IntervaloDAL

		Public oracleConnection As OracleConnection

		Public Sub New()
			oracleConnection = New OracleConnection(Utils.ORACLE_CONNECCAO)

			oracleConnection.Open()
		End Sub
		Public Function selectIntervaloPorId(id As Integer) As IntervaloModel
			If True Then

				Dim sql As New IntervaloSQL()


				Dim command As New OracleCommand()
				command.Connection = oracleConnection
				command.CommandText = sql.selectIntervaloPorId()

				command.Parameters.Add("codigo", id)
				command.CommandType = System.Data.CommandType.Text
				Dim reader As OracleDataReader = command.ExecuteReader()
				Dim Intervalo As IntervaloModel = Nothing

				Try
					If reader.Read() Then
                        Intervalo = New IntervaloModel() With {
                            .codIntervalo = reader.GetInt32(0),
                            .inicio = reader.GetInt32(1),
                            .fim = reader.GetInt32(2),
                            .situacao = reader.GetString(3),
                            .codPeriodo = reader.GetInt32(4),
                            .situacaoLabel = If(reader.GetString(3) = "1", "Ativo", "Inativo")
                        }
                    End If

                    Return Intervalo
                Catch ex As Exception
                    Throw ex
                Finally

                    If oracleConnection IsNot Nothing Then
                        oracleConnection.Close()
                    End If
                End Try
            End If
        End Function

        Public Sub updateIntervalo(intervalo As IntervaloModel)
            Dim command As New OracleCommand()
            Dim transaction As OracleTransaction = oracleConnection.BeginTransaction()
            ' Assign transaction object for a pending local transaction
            command.Connection = oracleConnection
            command.Transaction = transaction
            Try
                Dim sql As New IntervaloSQL()
                command.CommandText = sql.updateIntervalo()
                command.Parameters.Add("codigo", intervalo.codIntervalo)
                command.Parameters.Add("inicio", intervalo.inicio)
                command.Parameters.Add("fim", intervalo.fim)
                command.Parameters.Add("periodo", intervalo.codPeriodo)
                command.ExecuteNonQuery()
                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()

                Throw ex
            End Try
        End Sub

        Public Sub deleteIntervalo(id As Integer)
            Dim command As New OracleCommand()
            Dim transaction As OracleTransaction = oracleConnection.BeginTransaction()
            ' Assign transaction object for a pending local transaction
            command.Connection = oracleConnection
            command.Transaction = transaction
            Try
                Dim sql As New IntervaloSQL()
                command.CommandText = sql.deleteIntervalo()
                command.Parameters.Add("codigo", id)
                command.ExecuteNonQuery()
                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()

                Throw ex
            End Try
        End Sub

        Public Function selectIntervalo(periodo As Integer) As List(Of IntervaloModel)

            Dim sql As New IntervaloSQL()


            Dim command As New OracleCommand()
            command.Connection = oracleConnection
            command.CommandText = sql.selectIntervalo()

            command.Parameters.Add("periodo", periodo)
            command.CommandType = System.Data.CommandType.Text
            Dim reader As OracleDataReader = command.ExecuteReader()
            Dim Intervalo As New List(Of IntervaloModel)()

            Try
                While reader.Read()
                    Intervalo.Add(New IntervaloModel() With {
                        .codIntervalo = reader.GetInt32(0),
                        .inicio = reader.GetInt32(1),
                        .fim = reader.GetInt32(2),
                        .situacao = reader.GetString(3),
                        .codPeriodo = reader.GetInt32(4)
                    })
                End While

				Return Intervalo
			Catch ex As Exception
				Throw ex
			Finally

				If oracleConnection IsNot Nothing Then
					oracleConnection.Close()
				End If
			End Try
		End Function
		Public Sub inserirIntervalo(intervalo As IntervaloModel)
			Dim command As New OracleCommand()
			Dim transaction As OracleTransaction = oracleConnection.BeginTransaction()
			' Assign transaction object for a pending local transaction
			command.Connection = oracleConnection
			command.Transaction = transaction
			Try
				Dim sql As New IntervaloSQL()
				command.CommandText = sql.inserirIntervalo()

				command.Parameters.Add("inicio", intervalo.inicio)
				command.Parameters.Add("fim", intervalo.fim)
				command.Parameters.Add("periodo", intervalo.codPeriodo)
				command.ExecuteNonQuery()
				transaction.Commit()
			Catch ex As Exception
				transaction.Rollback()

				Throw ex
			End Try
		End Sub
		Public Sub ativarIntervalo(codigo As Integer)
			Dim command As New OracleCommand()
			Dim transaction As OracleTransaction = oracleConnection.BeginTransaction()
			' Assign transaction object for a pending local transaction
			command.Connection = oracleConnection
			command.Transaction = transaction
			Try
				Dim sql As New IntervaloSQL()
				command.CommandText = sql.ativarIntervalo()
				command.Parameters.Add("codigo", codigo)
				command.ExecuteNonQuery()
				transaction.Commit()
			Catch ex As Exception
				transaction.Rollback()

				Throw ex
			End Try
		End Sub
		Public Sub desativarIntervalo(codigo As Integer)
			Dim command As New OracleCommand()
			Dim transaction As OracleTransaction = oracleConnection.BeginTransaction()
			' Assign transaction object for a pending local transaction
			command.Connection = oracleConnection
			command.Transaction = transaction
			Try
				Dim sql As New IntervaloSQL()
				command.CommandText = sql.desativarIntervalo()
				command.Parameters.Add("codigo", codigo)
				command.ExecuteNonQuery()
				transaction.Commit()
			Catch ex As Exception
				transaction.Rollback()

				Throw ex
			End Try
		End Sub
	End Class
End Namespace