<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">

	<!-- stuff specified to comments authored in DDUEXML -->

	<xsl:import href="utilities_bibliography.xsl"/>
	<xsl:param name="bibliographyData" select="'../Data/bibliography.xml'"/>

	<xsl:include href="utilities_reference.xsl" />

	<xsl:variable name="summary" select="normalize-space(/document/comments/summary)" />

	<xsl:template name="body">

		<!-- auto-inserted info -->
		<!-- <xsl:apply-templates select="/document/reference/attributes" /> -->
		<xsl:apply-templates select="/document/comments/preliminary" />
		<xsl:apply-templates select="/document/comments/summary" />
		<xsl:if test="$group='member' and /document/reference/elements">
			<xsl:apply-templates select="/document/reference/elements/element/overloads" />
		</xsl:if>
		<!-- syntax -->
		<xsl:apply-templates select="/document/syntax" />
		<!-- generic templates -->
		<xsl:apply-templates select="/document/reference/templates" />
		<!-- parameters & return value -->
		<xsl:apply-templates select="/document/reference/parameters" />
		<xsl:choose>
			<xsl:when test="/document/comments/value | /document/comments/returns">
				<xsl:apply-templates select="/document/comments/value" />
				<xsl:apply-templates select="/document/comments/returns" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="/document/reference/returns[1] | /document/reference/eventhandler/type">
					<xsl:call-template name="defaultReturnSection" />
				</xsl:if>
			</xsl:otherwise>
		</xsl:choose>
		<!-- usage note for extension methods -->
		<xsl:if test="/document/reference/attributes/attribute/type[@api='T:System.Runtime.CompilerServices.ExtensionAttribute'] and boolean($group='member')">
			<xsl:call-template name="section">
				<xsl:with-param name="title">
					<include item="extensionUsageTitle" />
				</xsl:with-param>
				<xsl:with-param name="content">
					<include item="extensionUsageText">
						<parameter>
							<xsl:apply-templates select="/document/reference/parameters/parameter[1]/type" mode="link" />
						</parameter>
					</include>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:if>

		<!-- members -->
		<xsl:choose>
			<xsl:when test="$tgroup='root'">
				<xsl:apply-templates select="/document/reference/elements" mode="root" />
			</xsl:when>
			<xsl:when test="$group='namespace'">
				<xsl:apply-templates select="/document/reference/elements" mode="namespace" />
			</xsl:when>
			<xsl:when test="$subgroup='enumeration'">
				<xsl:apply-templates select="/document/reference/elements" mode="enumeration" />
			</xsl:when>
			<xsl:when test="$group='type'">
				<xsl:apply-templates select="/document/reference/elements" mode="type" />
			</xsl:when>
			<xsl:when test="$group='member'">
				<xsl:apply-templates select="/document/reference/elements" mode="overload" />
			</xsl:when>
		</xsl:choose>
		<!-- remarks -->
		<xsl:apply-templates select="/document/comments/remarks" />
		<xsl:apply-templates select="/document/comments/threadsafety" />
		<!-- example -->
		<xsl:apply-templates select="/document/comments/example" />
		<!-- other comment sections -->
		<!-- permissions -->
		<xsl:call-template name="permissions" />
		<!-- events -->
		<xsl:call-template name="events" />
		<!-- exceptions -->
		<xsl:call-template name="exceptions" />
		<!-- inheritance -->
		<xsl:apply-templates select="/document/reference/family" />
		<!--versions-->
		<xsl:if test="not($group='list' or $group='namespace' or $tgroup='root' )">
			<xsl:apply-templates select="/document/reference/versions" />
		</xsl:if>
		<!-- bibliography -->
		<xsl:call-template name="bibliography" />
		<!-- see also -->
		<xsl:call-template name="seealso" />
		<!-- assembly information -->
		<!-- <xsl:apply-templates select="/document/reference/containers/library" /> -->

	</xsl:template>

	<xsl:template name="defaultReturnSection">
		<xsl:call-template name="section">
			<xsl:with-param name="title">
				<xsl:choose>
					<xsl:when test="/document/reference/apidata[@subgroup='property']">
						<include item="propertyValueTitle" />
					</xsl:when>
					<xsl:when test="/document/reference/apidata[@subgroup='field']">
						<include item="fieldValueTitle" />
					</xsl:when>
					<xsl:when test="/document/reference/apidata[@subgroup='event']">
						<include item="valueTitle" />
					</xsl:when>
					<xsl:otherwise>
						<include item="methodValueTitle" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:with-param>
			<xsl:with-param name="content">
				<xsl:choose>
					<xsl:when test="/document/reference/attributes/attribute/type[@api='T:System.Runtime.CompilerServices.FixedBufferAttribute']">
						<xsl:apply-templates select="/document/reference/attributes/attribute/type[@api='T:System.Runtime.CompilerServices.FixedBufferAttribute']/../argument/typeValue/type" mode="link" />
					</xsl:when>
					<xsl:when test="/document/reference/apidata[@subgroup='event']">
						<xsl:apply-templates select="/document/reference/eventhandler/type" mode="link" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:apply-templates select="/document/reference/returns[1]" mode="link" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template name="getParameterDescription">
		<xsl:param name="name" />
		<xsl:apply-templates select="/document/comments/param[@name=$name]" />
	</xsl:template>

	<xsl:template name="getReturnsDescription">
		<xsl:param name="name" />
		<xsl:apply-templates select="/document/comments/param[@name=$name]" />
	</xsl:template>

	<xsl:template name="getElementDescription">
		<xsl:apply-templates select="summary" />
	</xsl:template>

	<xsl:template name="getInternalOnlyDescription">
		<!-- To do -->
	</xsl:template>

	<!-- block sections -->

	<xsl:template match="summary">
		<div class="summary">
			<xsl:apply-templates />
		</div>
	</xsl:template>

	<xsl:template match="overloads">
		<xsl:choose>
			<xsl:when test="count(*) > 0">
				<xsl:apply-templates />
			</xsl:when>
			<xsl:otherwise>
				<div class="summary">
					<xsl:apply-templates/>
				</div>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="templates">
		<xsl:call-template name="section">
			<xsl:with-param name="title">
				<include item="templatesTitle" />
			</xsl:with-param>
			<xsl:with-param name="content">
				<dl>
					<xsl:for-each select="template">
						<xsl:variable name="templateName" select="@name" />
						<dt>
							<span class="parameter">
								<xsl:value-of select="$templateName"/>
							</span>
						</dt>
						<dd>
							<xsl:apply-templates select="/document/comments/typeparam[@name=$templateName]" />
						</dd>
					</xsl:for-each>
				</dl>
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="value">
		<xsl:call-template name="section">
			<xsl:with-param name="title">
				<xsl:choose>
					<xsl:when test="/document/reference/apidata[@subgroup='property']">
						<include item="propertyValueTitle" />
					</xsl:when>
					<xsl:otherwise>
						<include item="fieldValueTitle" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:with-param>
			<xsl:with-param name="content">
				<xsl:apply-templates select="/document/reference/returns[1]" mode="link" />
				<br />
				<xsl:apply-templates />
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="returns">
		<xsl:call-template name="section">
			<xsl:with-param name="title">
				<include item="methodValueTitle" />
			</xsl:with-param>
			<xsl:with-param name="content">
				<xsl:apply-templates select="/document/reference/returns[1]" mode="link" />
				<br />
				<xsl:apply-templates />
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="remarks">
		<xsl:call-template name="section">
			<xsl:with-param name="title">
				<include item="remarksTitle" />
			</xsl:with-param>
			<xsl:with-param name="content">
				<xsl:apply-templates />
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="example">
		<xsl:call-template name="section">
			<xsl:with-param name="title">
				<include item="examplesTitle" />
			</xsl:with-param>
			<xsl:with-param name="content">
				<!-- EFW - Removed grouping.  It was causing the loss of any information between examples such as
						 descriptive text and was hiding anything with an unrecognized language.  As such, just render
						 the content as given. -->
				<xsl:apply-templates />
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="para">
		<p>
			<xsl:apply-templates />
		</p>
	</xsl:template>

	<xsl:template match="code">
		<xsl:variable name="codeLang">
			<xsl:call-template name="getCodeLang">
				<xsl:with-param name="p_codeLang" select="@language" />
			</xsl:call-template>
		</xsl:variable>
		<div class="code" x-lang="{$codeLang}">
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
						<pre xml:space="preserve"><xsl:apply-templates/></pre>
					</td>
				</tr>
			</table>
		</div>
	</xsl:template>

	<xsl:template name="events">
		<xsl:if test="count(/document/comments/event) &gt; 0">
			<xsl:call-template name="section">
				<xsl:with-param name="title">
					<include item="eventsTitle" />
				</xsl:with-param>
				<xsl:with-param name="content">
					<table class="exceptions">
						<tr>
							<th class="exceptionNameColumn">
								<include item="eventTypeHeader" />
							</th>
							<th class="exceptionConditionColumn">
								<include item="eventReasonHeader" />
							</th>
						</tr>
						<xsl:for-each select="/document/comments/event">
							<tr>
								<td>
									<referenceLink target="{@cref}" />
								</td>
								<td>
									<xsl:apply-templates select="." />
									<br />
								</td>
							</tr>
						</xsl:for-each>
					</table>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<xsl:template name="exceptions">
		<xsl:if test="count(/document/comments/exception) &gt; 0">
			<xsl:call-template name="section">
				<xsl:with-param name="title">
					<include item="exceptionsTitle" />
				</xsl:with-param>
				<xsl:with-param name="content">
					<table class="exceptions">
						<tr>
							<th class="exceptionNameColumn">
								<include item="exceptionNameHeader" />
							</th>
							<th class="exceptionConditionColumn">
								<include item="exceptionConditionHeader" />
							</th>
						</tr>
						<xsl:for-each select="/document/comments/exception">
							<tr>
								<td>
									<referenceLink target="{@cref}" />
								</td>
								<td>
									<xsl:apply-templates select="." />
									<br />
								</td>
							</tr>
						</xsl:for-each>
					</table>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<xsl:template name="permissions">
		<xsl:if test="count(/document/comments/permission) &gt; 0">
			<xsl:call-template name="section">
				<xsl:with-param name="title">
					<include item="permissionsTitle" />
				</xsl:with-param>
				<xsl:with-param name="content">
					<table class="permissions">
						<tr>
							<th class="permissionNameColumn">
								<include item="permissionNameHeader" />
							</th>
							<th class="permissionDescriptionColumn">
								<include item="permissionDescriptionHeader" />
							</th>
						</tr>
						<xsl:for-each select="/document/comments/permission">
							<tr>
								<td>
									<referenceLink target="{@cref}" />
								</td>
								<td>
									<xsl:apply-templates select="." />
									<br />
								</td>
							</tr>
						</xsl:for-each>
					</table>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<xsl:template name="seealso">
		<!-- EFW: For comments//seealso, exclude any nested in an overloads element -->
		<xsl:if test="count(/document/comments//seealso[not(ancestor::overloads)] |
			/document/comments/conceptualLink |
			/document/reference/elements/element/overloads//seealso) &gt; 0">
			<xsl:call-template name="section">
				<xsl:with-param name="title">
					<include item="relatedTitle" />
				</xsl:with-param>
				<xsl:with-param name="content">
					<xsl:for-each select="/document/comments//seealso[not(ancestor::overloads)] | /document/reference/elements/element/overloads//seealso">
						<xsl:apply-templates select=".">
							<xsl:with-param name="displaySeeAlso" select="true()" />
						</xsl:apply-templates>
						<br />
					</xsl:for-each>
					<!-- EFW: Copy conceptualLink elements as-is -->
					<xsl:for-each select="/document/comments/conceptualLink">
						<div class="seeAlsoStyle">
							<xsl:copy-of select="."/>
						</div>
					</xsl:for-each>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<xsl:template match="list[@type='bullet']">
		<ul>
			<xsl:for-each select="item">
				<li>
					<xsl:choose>
						<xsl:when test="term or description">
							<xsl:if test="term">
								<b>
									<xsl:apply-templates select="term" />
								</b>
								<xsl:text> - </xsl:text>
							</xsl:if>
							<xsl:apply-templates select="description" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates />
						</xsl:otherwise>
					</xsl:choose>
				</li>
			</xsl:for-each>
		</ul>
	</xsl:template>

	<xsl:template match="list[@type='number']">
		<ol>
			<xsl:if test="@start">
				<xsl:attribute name="start">
					<xsl:value-of select="@start"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:for-each select="item">
				<li>
					<xsl:choose>
						<xsl:when test="term or description">
							<xsl:if test="term">
								<b>
									<xsl:apply-templates select="term" />
								</b>
								<xsl:text> - </xsl:text>
							</xsl:if>
							<xsl:apply-templates select="description" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates />
						</xsl:otherwise>
					</xsl:choose>
				</li>
			</xsl:for-each>
		</ol>
	</xsl:template>

	<xsl:template match="list[@type='table']">
		<table class="authoredTable">
			<xsl:for-each select="listheader">
				<tr>
					<xsl:for-each select="*">
						<th>
							<xsl:apply-templates />
						</th>
					</xsl:for-each>
				</tr>
			</xsl:for-each>
			<xsl:for-each select="item">
				<tr>
					<xsl:for-each select="*">
						<td>
							<xsl:apply-templates />
							<br />
						</td>
					</xsl:for-each>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>

	<xsl:template match="list[@type='definition']">
		<dl class="authored">
			<xsl:for-each select="item">
				<dt>
					<xsl:apply-templates select="term" />
				</dt>
				<dd>
					<xsl:apply-templates select="description" />
				</dd>
			</xsl:for-each>
		</dl>
	</xsl:template>

	<!-- inline tags -->

	<xsl:template match="conceptualLink">
		<xsl:copy>
			<xsl:copy-of select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="see[@cref]">
		<xsl:choose>
			<xsl:when test="starts-with(@cref,'O:')">
				<referenceLink target="{concat('Overload:',substring(@cref,3))}">
					<xsl:value-of select="." />
				</referenceLink>
			</xsl:when>
			<xsl:when test="normalize-space(.)">
				<referenceLink target="{@cref}">
					<xsl:value-of select="." />
				</referenceLink>
			</xsl:when>
			<xsl:otherwise>
				<referenceLink target="{@cref}"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="see[@href]">
		<xsl:call-template name="hyperlink">
			<xsl:with-param name="content" select="."/>
			<xsl:with-param name="href" select="@href"/>
			<xsl:with-param name="target" select="@target"/>
			<xsl:with-param name="alt" select="@alt"/>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="seealso[@href]">
		<xsl:param name="displaySeeAlso" select="false()" />
		<xsl:if test="$displaySeeAlso">
			<xsl:call-template name="hyperlink">
				<xsl:with-param name="content" select="."/>
				<xsl:with-param name="href" select="@href"/>
				<xsl:with-param name="target" select="@target"/>
				<xsl:with-param name="alt" select="@alt"/>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<xsl:template name="hyperlink">
		<xsl:param name="content"/>
		<xsl:param name="href"/>
		<xsl:param name="target"/>
		<xsl:param name="alt"/>
		<a>
			<xsl:attribute name="href">
				<xsl:value-of select="$href"/>
			</xsl:attribute>
			<xsl:choose>
				<xsl:when test="normalize-space($target)">
					<xsl:attribute name="target">
						<xsl:value-of select="normalize-space($target)"/>
					</xsl:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="target">_blank</xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:if test="normalize-space($alt)">
				<xsl:attribute name="title">
					<xsl:value-of select="normalize-space($alt)"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:choose>
				<xsl:when test="normalize-space($content)">
					<xsl:value-of select="$content" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$href" />
				</xsl:otherwise>
			</xsl:choose>
		</a>
	</xsl:template>

	<xsl:template match="see[@langword]">
		<span class="keyword">
			<xsl:choose>
				<xsl:when test="@langword='null' or @langword='Nothing' or @langword='nullptr'">
					<span class="cs">null</span>
					<span class="vb">Nothing</span>
					<span class="cpp">nullptr</span>
				</xsl:when>
				<xsl:when test="@langword='static' or @langword='Shared'">
					<span class="cs">static</span>
					<span class="vb">Shared</span>
					<span class="cpp">static</span>
				</xsl:when>
				<xsl:when test="@langword='virtual' or @langword='Overridable'">
					<span class="cs">virtual</span>
					<span class="vb">Overridable</span>
					<span class="cpp">virtual</span>
				</xsl:when>
				<xsl:when test="@langword='true' or @langword='True'">
					<span class="cs">true</span>
					<span class="vb">True</span>
					<span class="cpp">true</span>
				</xsl:when>
				<xsl:when test="@langword='false' or @langword='False'">
					<span class="cs">false</span>
					<span class="vb">False</span>
					<span class="cpp">false</span>
				</xsl:when>
				<xsl:when test="@langword='abstract' or @langword='MustInherit'">
					<span class="cs">abstract</span>
					<span class="vb">MustInherit</span>
					<span class="cpp">abstract</span>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="@langword" />
				</xsl:otherwise>
			</xsl:choose>
		</span>
	</xsl:template>

	<xsl:template match="seealso">
		<xsl:param name="displaySeeAlso" select="false()" />
		<xsl:if test="$displaySeeAlso">
			<xsl:choose>
				<xsl:when test="starts-with(@cref,'O:')">
					<referenceLink target="{concat('Overload:',substring(@cref,3))}">
						<xsl:value-of select="." />
					</referenceLink>
				</xsl:when>
				<xsl:when test="normalize-space(.)">
					<referenceLink target="{@cref}">
						<xsl:value-of select="." />
					</referenceLink>
				</xsl:when>
				<xsl:otherwise>
					<referenceLink target="{@cref}" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
	</xsl:template>

	<xsl:template match="c">
		<span class="code">
			<xsl:apply-templates />
		</span>
	</xsl:template>

	<xsl:template match="paramref">
		<span class="parameter">
			<xsl:value-of select="@name" />
		</span>
	</xsl:template>

	<xsl:template match="typeparamref">
		<span class="typeparameter">
			<xsl:value-of select="@name" />
		</span>
	</xsl:template>

	<!-- pass through html tags -->

	<xsl:template match="p|ol|ul|li|dl|dt|dd|table|tr|th|td|h1|h2|h3|h4|h5|h6|hr|br|pre|blockquote|div|span|a|img|b|i|strong|em|del|sub|sup|abbr|acronym|u|font|map|area">
		<xsl:copy>
			<xsl:copy-of select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<!-- extra tag support -->

	<xsl:template match="threadsafety">
		<xsl:call-template name="section">
			<xsl:with-param name="title">
				<include item="threadSafetyTitle" />
			</xsl:with-param>
			<xsl:with-param name="content">
				<xsl:choose>
					<xsl:when test="normalize-space(.)">
						<xsl:apply-templates />
					</xsl:when>
					<xsl:otherwise>
						<xsl:if test="@static='true'">
							<include item="staticThreadSafe" />
						</xsl:if>
						<xsl:if test="@static='false'">
							<include item="staticNotThreadSafe" />
						</xsl:if>
						<xsl:if test="@instance='true'">
							<include item="instanceThreadSafe" />
						</xsl:if>
						<xsl:if test="@instance='false'">
							<include item="instanceNotThreadSafe" />
						</xsl:if>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="note">
		<xsl:variable name="title">
			<xsl:choose>
				<xsl:when test="@type='note'">
					<xsl:text>noteTitle</xsl:text>
				</xsl:when>
				<xsl:when test="@type='tip'">
					<xsl:text>tipTitle</xsl:text>
				</xsl:when>
				<xsl:when test="@type='caution' or @type='warning'">
					<xsl:text>cautionTitle</xsl:text>
				</xsl:when>
				<xsl:when test="@type='security' or @type='security note'">
					<xsl:text>securityTitle</xsl:text>
				</xsl:when>
				<xsl:when test="@type='important'">
					<xsl:text>importantTitle</xsl:text>
				</xsl:when>
				<xsl:when test="@type='vb' or @type='VB' or @type='VisualBasic' or @type='visual basic note'">
					<xsl:text>visualBasicTitle</xsl:text>
				</xsl:when>
				<xsl:when test="@type='cs' or @type='CSharp' or @type='c#' or @type='C#' or @type='visual c# note'">
					<xsl:text>visualC#Title</xsl:text>
				</xsl:when>
				<xsl:when test="@type='cpp' or @type='c++' or @type='C++' or @type='CPP' or @type='visual c++ note'">
					<xsl:text>visualC++Title</xsl:text>
				</xsl:when>
				<xsl:when test="@type='JSharp' or @type='j#' or @type='J#' or @type='visual j# note'">
					<xsl:text>visualJ#Title</xsl:text>
				</xsl:when>
				<xsl:when test="@type='implement'">
					<xsl:text>NotesForImplementersTitle</xsl:text>
				</xsl:when>
				<xsl:when test="@type='caller'">
					<xsl:text>NotesForCallers</xsl:text>
				</xsl:when>
				<xsl:when test="@type='inherit'">
					<xsl:text>NotesForInheritorsTitle</xsl:text>
				</xsl:when>
				<xsl:otherwise>
					<xsl:text>noteTitle</xsl:text>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="altTitle">
			<xsl:choose>
				<xsl:when test="@type='note' or @type='implement' or @type='caller' or @type='inherit'">
					<xsl:text>noteAltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='tip'">
					<xsl:text>tipAltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='caution' or @type='warning'">
					<xsl:text>cautionAltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='security' or @type='security note'">
					<xsl:text>securityAltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='important'">
					<xsl:text>importantAltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='vb' or @type='VB' or @type='VisualBasic' or @type='visual basic note'">
					<xsl:text>visualBasicAltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='cs' or @type='CSharp' or @type='c#' or @type='C#' or @type='visual c# note'">
					<xsl:text>visualC#AltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='cpp' or @type='c++' or @type='C++' or @type='CPP' or @type='visual c++ note'">
					<xsl:text>visualC++AltText</xsl:text>
				</xsl:when>
				<xsl:when test="@type='JSharp' or @type='j#' or @type='J#' or @type='visual j# note'">
					<xsl:text>visualJ#AltText</xsl:text>
				</xsl:when>
				<xsl:otherwise>
					<xsl:text>noteAltText</xsl:text>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="noteImg">
			<xsl:choose>
				<xsl:when test="@type='note' or @type='tip' or @type='implement' or @type='caller' or @type='inherit'">
					<xsl:text>alert_note.gif</xsl:text>
				</xsl:when>
				<xsl:when test="@type='caution' or @type='warning'">
					<xsl:text>alert_caution.gif</xsl:text>
				</xsl:when>
				<xsl:when test="@type='security' or @type='security note'">
					<xsl:text>alert_security.gif</xsl:text>
				</xsl:when>
				<xsl:when test="@type='important'">
					<xsl:text>alert_caution.gif</xsl:text>
				</xsl:when>
				<xsl:when test="@type='vb' or @type='VB' or @type='VisualBasic' or @type='visual basic note'">
					<xsl:text>alert_note.gif</xsl:text>
				</xsl:when>
				<xsl:when test="@type='cs' or @type='CSharp' or @type='c#' or @type='C#' or @type='visual c# note'">
					<xsl:text>alert_note.gif</xsl:text>
				</xsl:when>
				<xsl:when test="@type='cpp' or @type='c++' or @type='C++' or @type='CPP' or @type='visual c++ note'">
					<xsl:text>alert_note.gif</xsl:text>
				</xsl:when>
				<xsl:when test="@type='JSharp' or @type='j#' or @type='J#' or @type='visual j# note'">
					<xsl:text>alert_note.gif</xsl:text>
				</xsl:when>
				<xsl:otherwise>
					<xsl:text>alert_note.gif</xsl:text>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<div class="alert">
			<table>
				<tr>
					<th>
						<img>
							<includeAttribute item="iconPath" name="src">
								<parameter>
									<xsl:value-of select="$noteImg"/>
								</parameter>
							</includeAttribute>
							<includeAttribute name="title" item="{$altTitle}" />
						</img>
						<xsl:text> </xsl:text>
						<include item="{$title}" />
					</th>
				</tr>
				<tr>
					<td>
						<xsl:apply-templates />
					</td>
				</tr>
			</table>
		</div>
	</xsl:template>

	<xsl:template match="preliminary">
		<div class="preliminary">
			<xsl:choose>
				<xsl:when test="normalize-space(.)">
					<xsl:apply-templates/>
				</xsl:when>
				<xsl:otherwise>
					<include item="preliminaryText" />
				</xsl:otherwise>
			</xsl:choose>
		</div>
	</xsl:template>

	<xsl:template name="createReferenceLink">
		<xsl:param name="id" />
		<xsl:param name="qualified" select="false()" />
		<b>
			<referenceLink target="{$id}" qualified="{$qualified}" />
		</b>
	</xsl:template>

	<xsl:template name="section">
		<xsl:param name="title" />
		<xsl:param name="content" />
		<div class="section">
			<div class="sectionTitle" onclick="toggleSection(this.parentNode)">
				<img>
					<includeAttribute name="src" item="iconPath">
						<parameter>collapse_all.gif</parameter>
					</includeAttribute>
				</img>
				<xsl:text> </xsl:text>
				<xsl:copy-of select="$title" />
			</div>
			<div class="sectionContent">
				<xsl:copy-of select="$content" />
			</div>
		</div>
	</xsl:template>

	<!-- Bibliography -->
	<xsl:key name="citations" match="//cite" use="text()" />

	<xsl:variable name="hasCitations" select="boolean(count(//cite) > 0)"/>

	<xsl:template match="cite">
		<xsl:variable name="currentCitation" select="text()"/>
		<xsl:for-each select="//cite[generate-id(.)=generate-id(key('citations',text()))]">
			<!-- Distinct citations only -->
			<xsl:if test="$currentCitation=.">
				<xsl:choose>
					<xsl:when test="document($bibliographyData)/bibliography/reference[@name=$currentCitation]">
						<sup class="citation">
							<a>
								<xsl:attribute name="href">
									#cite<xsl:value-of select="position()"/>
								</xsl:attribute>[<xsl:value-of select="position()"/>]
							</a>
						</sup>
					</xsl:when>
					<xsl:otherwise>
						<xsl:apply-templates/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>

	<xsl:template name="bibliography">
		<xsl:if test="$hasCitations">
			<xsl:call-template name="section">
				<xsl:with-param name="toggleSwitch" select="'cite'" />
				<xsl:with-param name="title">
					<include item="bibliographyTitle"/>
				</xsl:with-param>
				<xsl:with-param name="content">
					<xsl:call-template name="autogenBibliographyLinks"/>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<xsl:template name="autogenBibliographyLinks">
		<xsl:for-each select="//cite[generate-id(.)=generate-id(key('citations',text()))]">
			<!-- Distinct citations only -->
			<xsl:variable name="citation" select="."/>
			<xsl:variable name="entry" select="document($bibliographyData)/bibliography/reference[@name=$citation]"/>

			<xsl:call-template name="bibliographyReference">
				<xsl:with-param name="number" select="position()"/>
				<xsl:with-param name="data" select="$entry"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>

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

	<!-- Pass through a chunk of markup.  This will allow build components to add HTML or other elements such as
			 "include" for localized shared content to a pre-transformed document.  This prevents it being removed as
			 unrecognized content by the transformations. -->
	<xsl:template match="markup">
		<xsl:copy-of select="node()"/>
	</xsl:template>
</xsl:stylesheet>
