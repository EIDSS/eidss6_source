<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0"
				xmlns:MSHelp="http://msdn.microsoft.com/mshelp"
        xmlns:mshelp="http://msdn.microsoft.com/mshelp"
				xmlns:ddue="http://ddue.schemas.microsoft.com/authoring/2003/5"
				xmlns:xlink="http://www.w3.org/1999/xlink"
        xmlns:msxsl="urn:schemas-microsoft-com:xslt"
        >

	<xsl:template name="getCodeLang">
		<xsl:param name="p_codeLang"/>
		<xsl:variable name="v_codeLangLC" select="translate($p_codeLang, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')"/>
		<xsl:choose>
			<xsl:when test="$v_codeLangLC = 'vbs' or $v_codeLangLC = 'vbscript'">
				<xsl:text>VBScript</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'vb' or $v_codeLangLC = 'vb#' or $v_codeLangLC = 'vb.net' or $v_codeLangLC = 'vbnet' or $v_codeLangLC = 'kblangvb' or $v_codeLangLC = 'visualbasic'" >
				<xsl:text>VisualBasic</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'visualbasicdeclaration'" >
				<xsl:text>VisualBasicDeclaration</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'visualbasicusage'" >
				<xsl:text>VisualBasicUsage</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'c#' or $v_codeLangLC = 'cs' or $v_codeLangLC = 'csharp'" >
				<xsl:text>CSharp</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'vb-c#' or $v_codeLangLC = 'visualbasicandcsharp'">
				<xsl:text>visualbasicANDcsharp</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'cpp' or $v_codeLangLC = 'cpp#' or $v_codeLangLC = 'c' or $v_codeLangLC = 'c++' or $v_codeLangLC = 'kblangcpp' or $v_codeLangLC = 'managedcplusplus'" >
				<xsl:text>ManagedCPlusPlus</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'j#' or $v_codeLangLC = 'jsharp'">
				<xsl:text>JSharp</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'f#' or $v_codeLangLC = 'fs' or $v_codeLangLC = 'fsharp' or $v_codeLangLC = 'fsscript'">
				<xsl:text>FSharp</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'jscript#' or $v_codeLangLC = 'jscript' or $v_codeLangLC = 'kbjscript' or $v_codeLangLC = 'jscript.net'">
				<xsl:text>JScript</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'js' or $v_codeLangLC = 'javascript' or $v_codeLangLC = 'ecmascript'">
				<xsl:text>JavaScript</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'xml' or $v_codeLangLC = 'xmllang' or $v_codeLangLC = 'xsl'">
				<xsl:text>xmlLang</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'html' or $v_codeLangLC = 'htm'">
				<xsl:text>html</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'xaml'">
				<xsl:text>XAML</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'aspnet' or $v_codeLangLC = 'asp.net'">
				<xsl:text>AspNet</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'pshell' or $v_codeLangLC = 'powershell' or $v_codeLangLC = 'ps1'">
				<xsl:text>pshell</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'sql' or $v_codeLangLC = 'sqlserver' or $v_codeLangLC = 'sql server'">
				<xsl:text>sql</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'py'">
				<xsl:text>Python</xsl:text>
			</xsl:when>
			<xsl:when test="$v_codeLangLC = 'none'">
				<xsl:value-of select="$v_codeLangLC"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>other</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="autogenSeeAlsoLinks">

		<!-- a link to the containing type on all list and member topics -->
		<xsl:if test="($group='member' or $group='list')">
			<xsl:variable name="typeTopicId">
				<xsl:choose>
					<xsl:when test="/document/reference/topicdata/@typeTopicId">
						<xsl:value-of select="/document/reference/topicdata/@typeTopicId"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="/document/reference/containers/type/@api"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			<div class="seeAlsoStyle">
				<referenceLink target="{$typeTopicId}" display-target="format">
					<include item="SeeAlsoTypeLinkText">
						<parameter>{0}</parameter>
						<parameter>
							<xsl:choose>
								<xsl:when test="/document/reference/topicdata/@typeTopicId">
									<xsl:value-of select="/document/reference/apidata/@subgroup"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="/document/reference/containers/type/apidata/@subgroup"/>
								</xsl:otherwise>
							</xsl:choose>
						</parameter>
					</include>
				</referenceLink>
			</div>
		</xsl:if>

		<!-- a link to the type's All Members list -->
		<xsl:variable name="allMembersTopicId">
			<xsl:choose>
				<xsl:when test="/document/reference/topicdata/@allMembersTopicId">
					<xsl:value-of select="/document/reference/topicdata/@allMembersTopicId"/>
				</xsl:when>
				<xsl:when test="$group='member' or ($group='list' and $subgroup='overload')">
					<xsl:value-of select="/document/reference/containers/type/topicdata/@allMembersTopicId"/>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="normalize-space($allMembersTopicId) and not($allMembersTopicId=$key)">
			<div class="seeAlsoStyle">
				<referenceLink target="{$allMembersTopicId}" display-target="format">
					<include item="SeeAlsoMembersLinkText">
						<parameter>{0}</parameter>
					</include>
				</referenceLink>
			</div>
		</xsl:if>

		<xsl:if test="/document/reference/memberdata/@overload">
			<!-- a link to the overload topic -->
			<div class="seeAlsoStyle">
				<referenceLink target="{/document/reference/memberdata/@overload}" display-target="format" show-parameters="false">
					<include item="SeeAlsoOverloadLinkText">
						<parameter>{0}</parameter>
					</include>
				</referenceLink>
			</div>
		</xsl:if>

		<!-- a link to the namespace topic -->
		<xsl:variable name="namespaceId">
			<xsl:value-of select="/document/reference/containers/namespace/@api"/>
		</xsl:variable>
		<xsl:if test="normalize-space($namespaceId)">
			<div class="seeAlsoStyle">
				<referenceLink target="{$namespaceId}" display-target="format">
					<include item="SeeAlsoNamespaceLinkText">
						<parameter>{0}</parameter>
					</include>
				</referenceLink>
			</div>
		</xsl:if>

	</xsl:template>

	<xsl:variable name="typeId">
		<xsl:choose>
			<xsl:when test="/document/reference/topicdata[@group='api'] and /document/reference/apidata[@group='type']">
				<xsl:value-of select="$key"/>
			</xsl:when>
			<xsl:when test="/document/reference/topicdata/@typeTopicId">
				<xsl:value-of select="/document/reference/topicdata/@typeTopicId"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="/document/reference/containers/type/@api"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>

	<!-- indent by 2*n spaces -->
	<xsl:template name="indent">
		<xsl:param name="count" />
		<xsl:if test="$count &gt; 1">
			<xsl:text>&#160;&#160;</xsl:text>
			<xsl:call-template name="indent">
				<xsl:with-param name="count" select="$count - 1" />
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<!-- Gets the substring after the last occurence of a period in a given string -->
	<xsl:template name="subString">
		<xsl:param name="name" />

		<xsl:choose>
			<xsl:when test="contains($name, '.')">
				<xsl:call-template name="subString">
					<xsl:with-param name="name" select="substring-after($name, '.')" />
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$name" />
			</xsl:otherwise>
		</xsl:choose>

	</xsl:template>

	<xsl:template name="codeSection">
		<xsl:param name="codeLang" />
		<div class="code">
			<span codeLanguage="{$codeLang}">
				<table width="100%" cellspacing="0" cellpadding="0">
					<tr>
						<th>
							<xsl:choose>
								<xsl:when test="@title">
									<xsl:value-of select="@title" />
								</xsl:when>
								<xsl:when test="$codeLang != 'other' and $codeLang != 'none'">
									<include item="{$codeLang}"/>
								</xsl:when>
							</xsl:choose>
							<xsl:text>&#xa0;</xsl:text>
						</th>
						<th>
							<span class="copyCode" onclick="CopyCode(this)" onkeypress="CopyCode_CheckKey(this, event)" onmouseover="ChangeCopyCodeIcon(this)" onmouseout="ChangeCopyCodeIcon(this)" tabindex="0">
								<img class="copyCodeImage" name="ccImage" align="absmiddle">
									<includeAttribute name="alt" item="copyImage" />
									<includeAttribute name="title" item="copyImage" />
									<includeAttribute name="src" item="iconPath">
										<parameter>copycode.gif</parameter>
									</includeAttribute>
								</img>
								<include item="copyCode"/>
							</span>
						</th>
					</tr>
					<tr>
						<td colspan="2">
							<!-- alansm: fix bug 321956 - use apply-templates rather than copy-of, so ddue:codeFeaturedElement nodes are transformed -->
							<pre xml:space="preserve"><xsl:apply-templates/></pre>
						</td>
					</tr>
				</table>
			</span>
		</div>

	</xsl:template>

	<xsl:template name="nonScrollingRegionTypeLinks">
		<include item="nonScrollingTypeLinkText">
			<parameter>{0}</parameter>
			<parameter>
				<xsl:choose>
					<xsl:when test="/document/reference/topicdata/@typeTopicId">
						<xsl:value-of select="/document/reference/apidata/@subgroup"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="/document/reference/containers/type/apidata/@subgroup"/>
					</xsl:otherwise>
				</xsl:choose>
			</parameter>
		</include>
	</xsl:template>

	<xsl:template name="mshelpCodelangAttributes">
		<xsl:param name="snippets" />
		<xsl:for-each select="$snippets">

			<xsl:if test="not(@language=preceding::*/@language)">
				<xsl:variable name="codeLang">
					<xsl:choose>
						<xsl:when test="@language = 'VBScript' or @language = 'vbs'">
							<xsl:text>VBScript</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'VisualBasic' or @language = 'vb' or @language = 'vb#' or @language = 'VB' or @language = 'kbLangVB'" >
							<xsl:text>kbLangVB</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'CSharp' or @language = 'c#' or @language = 'cs' or @language = 'C#'" >
							<xsl:text>CSharp</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'ManagedCPlusPlus' or @language = 'cpp' or @language = 'cpp#' or @language = 'c' or @language = 'c++' or @language = 'C++' or @language = 'kbLangCPP'" >
							<xsl:text>kbLangCPP</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'JSharp' or @language = 'j#' or @language = 'jsharp' or @language = 'VJ#'">
							<xsl:text>VJ#</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'JScript' or @language = 'js' or @language = 'jscript#' or @language = 'jscript' or @language = 'JScript' or @language = 'kbJScript'">
							<xsl:text>kbJScript</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'XAML' or @language = 'xaml'">
							<xsl:text>XAML</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'JavaScript' or @language = 'javascript'">
							<xsl:text>JavaScript</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'xml'">
							<xsl:text>xml</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'html'">
							<xsl:text>html</xsl:text>
						</xsl:when>
						<xsl:when test="@language = 'vb-c#'">
							<xsl:text>visualbasicANDcsharp</xsl:text>
						</xsl:when>
						<xsl:otherwise>
							<xsl:text>other</xsl:text>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$codeLang='other'" />
					<!-- If $codeLang is already authored, then do nothing -->
					<xsl:when test="/document/metadata/attribute[@name='codelang']/text() = $codeLang" />
					<xsl:otherwise>
						<xsl:call-template name="codeLang">
							<xsl:with-param name="codeLang" select="$codeLang" />
						</xsl:call-template>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>

		</xsl:for-each>
	</xsl:template>

	<xsl:template name="codeLang">
		<xsl:param name="codeLang" />
		<MSHelp:Attr Name="codelang" Value="{$codeLang}" />
	</xsl:template>

	<xsl:template name="trimAtPeriod">
		<xsl:param name="string" />

		<xsl:variable name="trimmedString" select="substring(normalize-space($string), 1, 256)" />
		<xsl:choose>
			<xsl:when test="normalize-space($string) != $trimmedString">
				<xsl:choose>
					<xsl:when test="not(contains($trimmedString, '.'))">
						<xsl:value-of select="$trimmedString"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="substringAndLastPeriod">
							<xsl:with-param name="string" select="$trimmedString" />
						</xsl:call-template>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="normalize-space($string)"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="substringAndLastPeriod">
		<xsl:param name="string" />

		<xsl:if test="contains($string, '.')">
			<xsl:variable name="after" select="substring-after($string, '.')" />
			<xsl:value-of select="concat(substring-before($string, '.'),'.')" />
			<xsl:if test="contains($after, '.')">
				<xsl:call-template name="substringAndLastPeriod">
					<xsl:with-param name="string" select="$after" />
				</xsl:call-template>
			</xsl:if>
		</xsl:if>
	</xsl:template>


</xsl:stylesheet>
