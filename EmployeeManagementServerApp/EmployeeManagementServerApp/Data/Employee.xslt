<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
>
	<xsl:template match="/">
				<style>
					table {text-align:center; }
					th {text-align:center}
					body{font-family:Trebuchet,Calibri,Arial; color:#262626}
				</style>
				<h2>Employees Data</h2>
				<table border="1" width="100%">
					<tr bgcolor="#D9D9D9">
						<th>Id</th>
						<th>Name</th>
						<th>Phone</th>
						<th>Department</th>
						<th>
							Address:
							Street
						</th>
						<th>
							Address:
							City
						</th>
						<th>
							Address:
							State
						</th>
						<th>
							Address:
							ZIP
						</th>
						<th>
							Address:
							Country
						</th>
					</tr>
					<xsl:for-each select="People/Employee">
						<tr>
							<td>
								<xsl:value-of select="@Id" />
							</td>
							<td>
								<xsl:value-of select="Name"/>
							</td>
							<td>
								<xsl:value-of select="Phone"/>
							</td>
							<td>
								<xsl:value-of select="DepartmentId"/>
							</td>
							<td>
								<xsl:value-of select="Address/Street"/>
							</td>
							<td>
								<xsl:value-of select="Address/City"/>
							</td>
							<td>
								<xsl:value-of select="Address/State"/>
							</td>
							<td>
								<xsl:value-of select="Address/ZIP"/>
							</td>
							<td>
								<xsl:value-of select="Address/Country"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
	</xsl:template>
</xsl:stylesheet>