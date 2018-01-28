<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" encoding="utf-8" indent="yes"/>
    <xsl:strip-space elements="*"/>

    <xsl:template match="@*|node()">
      <xsl:copy>
        <xsl:apply-templates select="@*|node()" />
      </xsl:copy>
    </xsl:template>

    <xsl:template match="author">
      <xsl:element name="author">
         <xsl:element name="first-name">	 
           <xsl:value-of select="@first-name" />	
         </xsl:element>
         <xsl:element name="last-name">
           <xsl:value-of select="last-name" />	
         </xsl:element>
      </xsl:element>
    </xsl:template>

</xsl:stylesheet>