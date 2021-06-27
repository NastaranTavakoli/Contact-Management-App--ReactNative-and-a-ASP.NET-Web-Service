<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
>
    <xsl:template match="/">
		<style>
			table {text-align:center; }
			th {text-align:center}
			body{font-family:Trebuchet,Calibri,Arial; color:#262626}
		</style>
          <h2>Departments</h2>
		<table border="1">
			<tr bgcolor="#D9D9D9">
				<th>Id</th>
				<th>Name</th>
			</tr>
			<xsl:for-each select="Departments/Department">
				<tr>
					<td width="50px">
						<xsl:value-of select="@Id" />
					</td>
					<td>
						<xsl:value-of select="Name"/>
					</td>
				</tr>
			</xsl:for-each>
		</table>
    </xsl:template>
</xsl:stylesheet>
