<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0"
  			xmlns:MSHelp="http://msdn.microsoft.com/mshelp"
        xmlns:mshelp="http://msdn.microsoft.com/mshelp"
				xmlns:ddue="http://ddue.schemas.microsoft.com/authoring/2003/5"
				xmlns:xlink="http://www.w3.org/1999/xlink"
        xmlns:msxsl="urn:schemas-microsoft-com:xslt"
   >

	<xsl:template name="insertMetadata">
		<xsl:if test="$metadata='true'">
			<xml>
				<xsl:call-template name="mshelpTitles" />
				<MSHelp:Attr Name="AssetID" Value="{$key}" />
				<!-- toc metadata -->
				<xsl:call-template name="linkMetadata" />
				<xsl:call-template name="indexMetadata" />
				<xsl:call-template name="helpMetadata" />
				<xsl:call-template name="helpPriorityMetadata" />
				<xsl:call-template name="apiTaggingMetadata" />
				<xsl:call-template name="mshelpDevlangAttributes" />
				<MSHelp:Attr Name="Locale">
					<includeAttribute name="Value" item="locale" />
				</MSHelp:Attr>
				<!-- attribute to allow F1 help integration -->
				<MSHelp:Attr Name="TopicType">
					<includeAttribute name="Value" item="TT_ManagedReference"/>
				</MSHelp:Attr>

				<!-- Abstract -->
				<xsl:choose>
					<xsl:when test="string-length(normalize-space($abstractSummary)) &gt; 254">
						<MSHelp:Attr Name="Abstract" Value="{concat(substring(normalize-space($abstractSummary),1,250), ' ...')}" />
					</xsl:when>
					<xsl:when test="string-length(normalize-space($abstractSummary)) &gt; 0 and $abstractSummary != '&#160;'">
						<MSHelp:Attr Name="Abstract" Value="{normalize-space($abstractSummary)}" />
					</xsl:when>
				</xsl:choose>

				<xsl:call-template name="mshelpCodelangAttributes" />
				<xsl:call-template name="versionMetadata" />
				<xsl:call-template name="authoredMetadata" />
			</xml>
		</xsl:if>
	</xsl:template>

	<!-- add DocSet and Technology attributes depending on the versions that support this api -->
	<xsl:template name="versionMetadata">
		<xsl:variable name="supportedOnCf">
			<xsl:call-template name="IsMemberSupportedOnCf"/>
		</xsl:variable>
		<xsl:if test="count(/document/reference/versions/versions[@name='netfw']/version) &gt; 0">
			<MSHelp:Attr Name="Technology">
				<includeAttribute name="Value" item="desktopTechnologyAttribute" />
			</MSHelp:Attr>
		</xsl:if>
		<xsl:if test="count(/document/reference/versions/versions[@name='netcfw']/version) &gt; 0 or normalize-space($supportedOnCf)!=''">
			<MSHelp:Attr Name="Technology">
				<includeAttribute name="Value" item="netcfTechnologyAttribute" />
			</MSHelp:Attr>
			<MSHelp:Attr Name="DocSet">
				<includeAttribute name="Value" item="netcfDocSetAttribute" />
			</MSHelp:Attr>
		</xsl:if>
	</xsl:template>

	<xsl:template name="authoredMetadata">

		<!-- authored attributes -->
		<xsl:for-each select="/document/metadata/attribute">
			<MSHelp:Attr Name="{@name}" Value="{text()}" />
		</xsl:for-each>

		<!-- authored K -->
		<xsl:for-each select="/document/metadata/keyword[@index='K']">
			<MSHelp:Keyword Index="K">
				<xsl:attribute name="Term">
					<xsl:value-of select="text()" />
					<xsl:for-each select="keyword[@index='K']">
						<xsl:text>, </xsl:text>
						<xsl:value-of select="text()"/>
					</xsl:for-each>
				</xsl:attribute>
			</MSHelp:Keyword>
		</xsl:for-each>

		<!-- authored F -->
		<xsl:for-each select="/document/metadata/keyword[@index='F']">
			<MSHelp:Keyword Index="F">
				<xsl:attribute name="Term">
					<xsl:value-of select="text()" />
					<xsl:for-each select="keyword[@index='F']">
						<xsl:text>, </xsl:text>
						<xsl:value-of select="text()"/>
					</xsl:for-each>
				</xsl:attribute>
			</MSHelp:Keyword>
		</xsl:for-each>

		<!-- authored B -->
		<xsl:for-each select="/document/metadata/keyword[@index='B']">
			<MSHelp:Keyword Index="B">
				<xsl:attribute name="Term">
					<xsl:value-of select="text()" />
					<xsl:for-each select="keyword[@index='B']">
						<xsl:text>, </xsl:text>
						<xsl:value-of select="text()"/>
					</xsl:for-each>
				</xsl:attribute>
			</MSHelp:Keyword>
		</xsl:for-each>

	</xsl:template>

	<xsl:template name="mshelpTitles">

		<!-- Toc List title-->
		<MSHelp:TOCTitle>
			<includeAttribute name="Title" item="tocTitle">
				<parameter>
					<xsl:call-template name="topicTitlePlain" />
				</parameter>
			</includeAttribute>
		</MSHelp:TOCTitle>

		<!-- The Results List title -->
		<MSHelp:RLTitle>
			<includeAttribute name="Title" item="rlTitle">
				<parameter>
					<xsl:call-template name="topicTitlePlain">
						<xsl:with-param name="qualifyMembers" select="true()" />
					</xsl:call-template>
				</parameter>
				<parameter>
					<xsl:value-of select="$namespaceName"/>
				</parameter>
			</includeAttribute>
		</MSHelp:RLTitle>

	</xsl:template>

	<xsl:template name="apiTaggingMetadata">
		<xsl:if test="($group='type' or $group='member')">
			<MSHelp:Attr Name="APIType" Value="Managed" />
			<MSHelp:Attr Name="APILocation" Value="{/document/reference/containers/library/@assembly}.dll" />
			<xsl:choose>
				<xsl:when test="$group='type'">
					<xsl:variable name="apiTypeName">
						<xsl:choose>
							<xsl:when test="/document/reference/containers/namespace/apidata/@name != ''">
								<xsl:value-of select="concat(/document/reference/containers/namespace/apidata/@name,'.',/document/reference/apidata/@name)" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="/document/reference/apidata/@name" />
							</xsl:otherwise>
						</xsl:choose>
						<xsl:if test="count(/document/reference/templates/template) > 0">
							<xsl:value-of select="concat('`',count(/document/reference/templates/template))" />
						</xsl:if>
					</xsl:variable>
					<!-- Namespace + Type -->
					<MSHelp:Attr Name="APIName" Value="{$apiTypeName}" />
					<xsl:choose>
						<xsl:when test="boolean($subgroup='delegate')">
							<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.ctor')}" />
							<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.','Invoke')}" />
							<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.','BeginInvoke')}" />
							<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.','EndInvoke')}" />
						</xsl:when>
						<xsl:when test="$subgroup='enumeration'">
							<xsl:for-each select="/document/reference/elements/element">
								<MSHelp:Attr Name="APIName" Value="{substring(@api,3)}" />
							</xsl:for-each>
							<!-- Namespace + Type + Member for each member -->
						</xsl:when>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="$group='member'">
					<xsl:variable name="apiTypeName">
						<xsl:value-of select="concat(/document/reference/containers/namespace/apidata/@name,'.',/document/reference/containers/type/apidata/@name)" />
						<xsl:if test="count(/document/reference/templates/template) > 0">
							<xsl:value-of select="concat('`',count(/document/reference/templates/template))" />
						</xsl:if>
					</xsl:variable>
					<!-- Namespace + Type + Member -->
					<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.',/document/reference/apidata/@name)}" />
					<xsl:choose>
						<!-- for properties, add APIName attribute get/set accessor methods -->
						<xsl:when test="boolean($subgroup='property')">
							<xsl:if test="/document/reference/propertydata[@get='true']">
								<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.get_',/document/reference/apidata/@name)}" />
							</xsl:if>
							<xsl:if test="/document/reference/propertydata[@set='true']">
								<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.set_',/document/reference/apidata/@name)}" />
							</xsl:if>
						</xsl:when>
						<!-- for events, add APIName attribute add/remove accessor methods -->
						<xsl:when test="boolean($subgroup='event')">
							<xsl:if test="/document/reference/eventdata[@add='true']">
								<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.add_',/document/reference/apidata/@name)}" />
							</xsl:if>
							<xsl:if test="/document/reference/eventdata[@remove='true']">
								<MSHelp:Attr Name="APIName" Value="{concat($apiTypeName,'.remove_',/document/reference/apidata/@name)}" />
							</xsl:if>
						</xsl:when>
					</xsl:choose>
				</xsl:when>
			</xsl:choose>
		</xsl:if>
	</xsl:template>

	<xsl:template name="linkMetadata">
		<!-- code entity reference keyword -->
		<MSHelp:Keyword Index="A" Term="{$key}" />

		<xsl:if test="$subgroup='enumeration'">
			<xsl:for-each select="/document/reference/elements/element">
				<MSHelp:Keyword Index="A" Term="{@api}" />
			</xsl:for-each>
		</xsl:if>

		<!-- frlrf keywords -->
		<xsl:choose>
			<xsl:when test="$group='namespace'">
				<MSHelp:Keyword Index="A" Term="{translate(concat('frlrf',/document/reference/apidata/@name),'.','')}" />
			</xsl:when>
			<!-- types & members, too -->
			<xsl:when test="$group='type'">
				<MSHelp:Keyword Index="A" Term="{translate(concat('frlrf',/document/reference/containers/namespace/apidata/@name, /document/reference/apidata/@name, 'ClassTopic'),'.','')}" />
			</xsl:when>
			<xsl:when test="$group='list'">
				<MSHelp:Keyword Index="A" Term="{translate(concat('frlrf',/document/reference/containers/namespace/apidata/@name, /document/reference/topicdata/@name, 'MembersTopic'),'.','')}" />
			</xsl:when>
			<xsl:when test="$group='member'">
				<MSHelp:Keyword Index="A" Term="{translate(concat('frlrf',/document/reference/containers/namespace/apidata/@name, /document/reference/containers/type/apidata/@name, 'Class', /document/reference/apidata/@name, 'Topic'),'.','')}" />
			</xsl:when>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="$group='namespace'">
				<MSHelp:Keyword Index="A" Term="{concat('frlrf',translate(@name,'.',''))}" />
			</xsl:when>
			<!-- types & members, too -->
		</xsl:choose>
	</xsl:template>

	<xsl:template name="oldIndexMetadata">
		<!-- K keywords -->
		<xsl:choose>
			<xsl:when test="$group='namespace'">
				<!-- namespace -->
				<xsl:variable name="namespace" select="/document/reference/apidata/@name" />
				<xsl:if test="boolean($namespace)">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="namespaceIndexEntry">
							<parameter>
								<xsl:value-of select="$namespace" />
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$group='type'">
				<!-- type -->
				<xsl:choose>
					<xsl:when test="count(/document/reference/templates/template) = 0">
						<!-- non-generic type -->
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:value-of select="/document/reference/apidata/@name" />
								</parameter>
								<parameter>
									<xsl:value-of select="$subgroup" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:value-of select="concat(/document/reference/containers/namespace[@api]/apidata/@name,'.',/document/reference/apidata/@name)" />
								</parameter>
								<parameter>
									<xsl:value-of select="$subgroup" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:when>
					<xsl:otherwise>
						<!-- generic type -->
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:value-of select="/document/reference/apidata/@name" />
									<xsl:for-each select="/document/reference/templates">
										<xsl:call-template name="csTemplatesInIndex" />
									</xsl:for-each>
								</parameter>
								<parameter>
									<xsl:value-of select="$subgroup" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:value-of select="/document/reference/apidata/@name" />
									<xsl:for-each select="/document/reference/templates">
										<xsl:call-template name="vbTemplates">
											<xsl:with-param name="seperator" select="string('%2C ')" />
										</xsl:call-template>
									</xsl:for-each>
								</parameter>
								<parameter>
									<xsl:value-of select="$subgroup" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:otherwise>
				</xsl:choose>
				<!-- an entry like: "FileClassifier class, about FileClassifier class" -->
				<xsl:if test="$subgroup='class' or $subgroup='structure' or $subgroup='interface'">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="aboutTypeIndexEntry">
							<parameter>
								<xsl:value-of select="/document/reference/apidata/@name" />
							</parameter>
							<parameter>
								<xsl:value-of select="$subgroup"/>
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
				</xsl:if>
				<!-- for enums, an entry for each enum member, e.g. "Sunken enumeration member" -->
				<xsl:if test="$subgroup='enumeration'">
					<xsl:for-each select="/document/reference/elements/element">
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:value-of select="apidata/@name" />
								</parameter>
								<parameter>
									<xsl:text>enumMember</xsl:text>
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:for-each>
				</xsl:if>
			</xsl:when>
			<xsl:when test="($group='member') and (starts-with($key,'Overload:') or not(/document/reference/memberdata/@overload))">
				<!-- member -->
				<xsl:variable name="indexEntryItem">
					<xsl:choose>
						<xsl:when test="boolean($subsubgroup)">
							<xsl:value-of select="$subsubgroup" />
						</xsl:when>
						<xsl:when test="boolean($subgroup)">
							<xsl:value-of select="$subgroup" />
						</xsl:when>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="count(/document/reference/templates/template) = 0">
						<!-- non-generic member -->
						<MSHelp:Keyword Index="K">
							<xsl:choose>
								<xsl:when test="$subgroup='constructor'">
									<includeAttribute name="Term" item="listTopicIndexEntry">
										<parameter>
											<xsl:value-of select="/document/reference/containers/type/apidata/@name"/>
										</parameter>
										<parameter>
											<xsl:value-of select="/document/reference/containers/type/apidata/@subgroup"/>
										</parameter>
										<parameter>
											<xsl:value-of select="$indexEntryItem" />
										</parameter>
									</includeAttribute>
								</xsl:when>
								<xsl:otherwise>
									<includeAttribute name="Term" item="nameSubgroupIndexEntry">
										<parameter>
											<xsl:value-of select="/document/reference/apidata/@name"/>
										</parameter>
										<parameter>
											<xsl:value-of select="$indexEntryItem" />
										</parameter>
									</includeAttribute>
								</xsl:otherwise>
							</xsl:choose>
						</MSHelp:Keyword>
					</xsl:when>
					<xsl:otherwise>
						<!-- generic member -->
						<MSHelp:Keyword Index="K">
							<xsl:choose>
								<xsl:when test="$subgroup='constructor'">
									<includeAttribute name="Term" item="listTopicIndexEntry">
										<parameter>
											<xsl:value-of select="/document/reference/containers/type/apidata/@name"/>
											<xsl:for-each select="/document/reference/templates">
												<xsl:call-template name="csTemplatesInIndex" />
											</xsl:for-each>
										</parameter>
										<parameter>
											<xsl:value-of select="/document/reference/containers/type/apidata/@subgroup"/>
										</parameter>
										<parameter>
											<xsl:value-of select="$indexEntryItem" />
										</parameter>
									</includeAttribute>
								</xsl:when>
								<xsl:otherwise>
									<includeAttribute name="Term" item="nameSubgroupIndexEntry">
										<parameter>
											<xsl:value-of select="/document/reference/apidata/@name"/>
											<xsl:for-each select="/document/reference/templates">
												<xsl:call-template name="csTemplatesInIndex" />
											</xsl:for-each>
										</parameter>
										<parameter>
											<xsl:value-of select="$indexEntryItem" />
										</parameter>
									</includeAttribute>
								</xsl:otherwise>
							</xsl:choose>
						</MSHelp:Keyword>
						<MSHelp:Keyword Index="K">
							<xsl:choose>
								<xsl:when test="$subgroup='constructor'">
									<includeAttribute name="Term" item="listTopicIndexEntry">
										<parameter>
											<xsl:value-of select="/document/reference/containers/type/apidata/@name"/>
											<xsl:for-each select="/document/reference/templates">
												<xsl:call-template name="vbTemplates">
													<xsl:with-param name="seperator" select="string('%2C ')" />
												</xsl:call-template>
											</xsl:for-each>
										</parameter>
										<parameter>
											<xsl:value-of select="/document/reference/containers/type/apidata/@subgroup"/>
										</parameter>
										<parameter>
											<xsl:value-of select="$indexEntryItem" />
										</parameter>
									</includeAttribute>
								</xsl:when>
								<xsl:otherwise>
									<includeAttribute name="Term" item="nameSubgroupIndexEntry">
										<parameter>
											<xsl:value-of select="/document/reference/apidata/@name"/>
											<xsl:for-each select="/document/reference/templates">
												<xsl:call-template name="vbTemplates">
													<xsl:with-param name="seperator" select="string('%2C ')" />
												</xsl:call-template>
											</xsl:for-each>
										</parameter>
										<parameter>
											<xsl:value-of select="$indexEntryItem" />
										</parameter>
									</includeAttribute>
								</xsl:otherwise>
							</xsl:choose>
						</MSHelp:Keyword>
					</xsl:otherwise>
				</xsl:choose>
				<!-- type + member -->
				<xsl:choose>
					<xsl:when test="count(/document/reference/containers/namespace[@api]/templates/template) = 0">
						<!-- non-generic type -->
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:choose>
										<xsl:when test="$subgroup='constructor'">
											<xsl:value-of select="concat(/document/reference/containers/type/apidata/@name,'.',/document/reference/containers/type/apidata/@name)" />
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="concat(/document/reference/containers/namespace[@api]/apidata/@name,'.',/document/reference/apidata/@name)" />
										</xsl:otherwise>
									</xsl:choose>
								</parameter>
								<parameter>
									<xsl:value-of select="$indexEntryItem" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:when>
					<xsl:otherwise>
						<!-- generic type -->
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:value-of select="/document/reference/containers/namespace[@api]/apidata/@name"/>
									<xsl:for-each select="/document/reference/containers/namespace[@api]/templates">
										<xsl:call-template name="vbTemplates">
											<xsl:with-param name="seperator" select="string('%2C ')" />
										</xsl:call-template>
									</xsl:for-each>
									<xsl:text>.</xsl:text>
									<xsl:choose>
										<xsl:when test="$subgroup='constructor'">
											<xsl:value-of select="/document/reference/type/apidata/@name" />
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="/document/reference/apidata/@name" />
										</xsl:otherwise>
									</xsl:choose>
								</parameter>
								<parameter>
									<xsl:value-of select="$indexEntryItem" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="nameSubgroupIndexEntry">
								<parameter>
									<xsl:value-of select="/document/reference/containers/namespace[@api]/apidata/@name"/>
									<xsl:for-each select="/document/reference/containers/namespace[@api]/templates">
										<xsl:call-template name="csTemplatesInIndex" />
									</xsl:for-each>
									<xsl:text>.</xsl:text>
									<xsl:choose>
										<xsl:when test="$subgroup='constructor'">
											<xsl:value-of select="/document/reference/type/apidata/@name" />
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="/document/reference/apidata/@name" />
										</xsl:otherwise>
									</xsl:choose>
								</parameter>
								<parameter>
									<xsl:value-of select="$indexEntryItem" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="($group='members' or $group='derivedtype')">
				<MSHelp:Keyword Index="K">
					<includeAttribute name="Term" item="listTopicIndexEntry">
						<parameter>
							<xsl:value-of select="/document/reference/apidata/@name" />
						</parameter>
						<parameter>
							<xsl:value-of select="/document/reference/containers/type[@api]/apidata/@subgroup"/>
						</parameter>
						<parameter>
							<xsl:value-of select="$subgroup" />
						</parameter>
					</includeAttribute>
				</MSHelp:Keyword>
			</xsl:when>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="helpMetadata">
		<!-- F keywords -->
		<xsl:choose>
			<!-- namespace pages get the namespace keyword, if it exists -->
			<xsl:when test="$group='namespace'">
				<xsl:variable name="namespace" select="/document/reference/apidata/@name" />
				<xsl:if test="boolean($namespace)">
					<MSHelp:Keyword Index="F" Term="{$namespace}" />
				</xsl:if>
			</xsl:when>
			<!-- type overview and member list pages get type and namespace.type keywords -->
			<xsl:when test="$group='type' or ($group='list' and $subgroup='members')">
				<xsl:variable name="namespace" select="/document/reference/containers/namespace/apidata/@name" />
				<xsl:variable name="type">
					<xsl:for-each select="/document/reference[1]">
						<xsl:call-template name="typeNamePlain">
							<xsl:with-param name="annotated" select="true()" />
						</xsl:call-template>
					</xsl:for-each>
				</xsl:variable>
				<MSHelp:Keyword Index="F" Term="{$type}" />
				<xsl:if test="boolean($namespace)">
					<MSHelp:Keyword Index="F" Term="{concat($namespace,'.',$type)}" />
				</xsl:if>
				<xsl:if test="$subgroup = 'enumeration'">
					<xsl:for-each select="/document/reference/elements/element">
						<MSHelp:Keyword Index="F" Term="{concat($type, '.', apidata/@name)}" />
						<xsl:if test="boolean($namespace)">
							<MSHelp:Keyword Index="F" Term="{concat($namespace,'.',$type, '.', apidata/@name)}" />
						</xsl:if>
					</xsl:for-each>
				</xsl:if>
				<xsl:call-template name="xamlMSHelpFKeywords"/>
			</xsl:when>
			<!-- member pages get member, type.member, and namepsace.type.member keywords -->
			<xsl:when test="$group='member'">
				<xsl:variable name="namespace" select="/document/reference/containers/namespace/apidata/@name" />
				<xsl:variable name="type">
					<xsl:for-each select="/document/reference/containers/type[1]">
						<xsl:call-template name="typeNamePlain">
							<xsl:with-param name="annotate" select="true()" />
						</xsl:call-template>
					</xsl:for-each>
				</xsl:variable>
				<xsl:variable name="member">
					<xsl:choose>
						<!-- if the member is a constructor, use the member name for the type name -->
						<xsl:when test="$subgroup='constructor'">
							<xsl:value-of select="$type" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="/document/reference/apidata/@name"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<MSHelp:Keyword Index="F" Term="{$member}" />
				<MSHelp:Keyword Index="F" Term="{concat($type, '.', $member)}" />
				<xsl:if test="boolean($namespace)">
					<MSHelp:Keyword Index="F" Term="{concat($namespace, '.', $type, '.', $member)}" />
				</xsl:if>
			</xsl:when>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="helpPriorityMetadata">
		<xsl:choose>
			<xsl:when test="$group='namespace' or $subgroup='members'">
				<MSHelp:Attr Name="HelpPriority" Value="1"/>
			</xsl:when>
			<xsl:when test="$group='type'">
				<MSHelp:Attr Name="HelpPriority" Value="2"/>
			</xsl:when>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="codeLang">
		<xsl:param name="codeLang" />
		<MSHelp:Attr Name="codelang" Value="{$codeLang}" />
	</xsl:template>

	<xsl:template name="mshelpDevlangAttributes">
		<xsl:for-each select="/document/syntax/div[@codeLanguage]">
			<xsl:if test="not(@codeLanguage=preceding::*/@codeLanguage)">
				<xsl:variable name="devlang">
					<xsl:choose>
						<xsl:when test="@codeLanguage = 'CSharp' or @codeLanguage = 'c#' or @codeLanguage = 'cs' or @codeLanguage = 'C#'" >
							<xsl:text>CSharp</xsl:text>
						</xsl:when>
						<xsl:when test="@codeLanguage = 'ManagedCPlusPlus' or @codeLanguage = 'cpp' or @codeLanguage = 'cpp#' or @codeLanguage = 'c' or @codeLanguage = 'c++' or @codeLanguage = 'C++' or @codeLanguage = 'kbLangCPP'" >
							<xsl:text>C++</xsl:text>
						</xsl:when>
						<xsl:when test="@codeLanguage = 'JScript' or @codeLanguage = 'js' or @codeLanguage = 'jscript#' or @codeLanguage = 'jscript' or @codeLanguage = 'JScript' or @codeLanguage = 'kbJScript'">
							<xsl:text>JScript</xsl:text>
						</xsl:when>
						<xsl:when test="@codeLanguage = 'VisualBasic' or @codeLanguage = 'vb' or @codeLanguage = 'vb#' or @codeLanguage = 'VB' or @codeLanguage = 'kbLangVB'" >
							<xsl:text>VB</xsl:text>
						</xsl:when>
						<xsl:when test="@codeLanguage = 'VBScript' or @codeLanguage = 'vbs'">
							<xsl:text>VBScript</xsl:text>
						</xsl:when>
						<xsl:when test="@codeLanguage = 'JSharp' or @codeLanguage = 'j#' or @codeLanguage = 'jsharp' or @codeLanguage = 'VJ#'">
							<xsl:text>VJ#</xsl:text>
						</xsl:when>
						<xsl:when test="@codeLanguage = 'xaml' or @codeLanguage = 'XAML'">
							<xsl:text>XAML</xsl:text>
						</xsl:when>
						<xsl:when test="@codeLanguage = 'xml' or @codeLanguage = 'XML'">
							<xsl:text>XML</xsl:text>
						</xsl:when>
						<xsl:otherwise>
							<xsl:text>other</xsl:text>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$devlang='other'" />
					<xsl:otherwise>
						<MSHelp:Attr Name="DevLang" Value="{$devlang}" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>

	<!-- 
     Additional F1 keywords for class, struct, and enum topics in a set of WPF namespaces. 
     This template inserts the MSHelp:Keyword nodes.
     The keyword prefixes and the WPF namespaces are hard-coded in variables.
	-->
	<xsl:variable name="var_wpf_f1index_prefix_1">http://schemas.microsoft.com/winfx/2006/xaml/presentation#</xsl:variable>
	<xsl:variable name="var_wpf_f1index_prefix_1_namespaces">N:System.Windows.Controls#N:System.Windows.Documents#N:System.Windows.Shapes#N:System.Windows.Navigation#N:System.Windows.Data#N:System.Windows#N:System.Windows.Controls.Primitives#N:System.Windows.Media.Animation#N:System.Windows.Annotations#N:System.Windows.Annotations.Anchoring#N:System.Windows.Annotations.Storage#N:System.Windows.Media#N:System.Windows.Media.Animation#N:System.Windows.Media.Media3D#N:</xsl:variable>

	<xsl:template name="xamlMSHelpFKeywords">
		<xsl:if test="$subgroup='class' or $subgroup='enumeration' or $subgroup='structure'">
			<xsl:if test="boolean(contains($var_wpf_f1index_prefix_1_namespaces, concat('#',/document/reference/containers/namespace/@api,'#'))
                           or starts-with($var_wpf_f1index_prefix_1_namespaces, concat(/document/reference/containers/namespace/@api,'#')))">
				<MSHelp:Keyword Index="F" Term="{concat($var_wpf_f1index_prefix_1, /document/reference/apidata/@name)}"/>
			</xsl:if>
		</xsl:if>
	</xsl:template>

	<!-- Index Logic -->

	<xsl:template name="indexMetadata">
		<xsl:choose>
			<!-- namespace topics get one unqualified index entry -->
			<xsl:when test="$group='namespace'">
				<xsl:variable name="names">
					<xsl:for-each select="/document/reference">
						<xsl:call-template name="textNames" />
					</xsl:for-each>
				</xsl:variable>
				<MSHelp:Keyword Index="K">
					<includeAttribute name="Term" item="namespaceIndexEntry">
						<parameter>
							<xsl:value-of select="msxsl:node-set($names)/name" />
						</parameter>
					</includeAttribute>
				</MSHelp:Keyword>
			</xsl:when>
			<!-- type overview topics get unqualified about -->
			<xsl:when test="$group='type'">
				<xsl:variable name="names">
					<xsl:for-each select="/document/reference">
						<xsl:call-template name="textNames" />
					</xsl:for-each>
				</xsl:variable>
				<xsl:variable name="namespace" select="/document/reference/containers/namespace/apidata/@name" />
				<xsl:for-each select="msxsl:node-set($names)/name">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="{$subgroup}IndexEntry">
							<parameter>
								<xsl:value-of select="." />
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
					<xsl:if test="boolean($namespace)">
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="{$subgroup}IndexEntry">
								<parameter>
									<xsl:value-of select="$namespace"/>
									<xsl:text>.</xsl:text>
									<xsl:value-of select="." />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:if>
					<xsl:if test="$subgroup = 'class' or $subgroup= 'structure' or $subgroup= 'interface'">
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="aboutTypeIndexEntry">
								<parameter>
									<include item="{$subgroup}IndexEntry">
										<parameter>
											<xsl:value-of select="." />
										</parameter>
									</include>
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:if>
				</xsl:for-each>
				<xsl:if test="$subgroup = 'enumeration'">
					<xsl:for-each select="/document/reference/elements/element">
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="{$subgroup}MemberIndexEntry">
								<parameter>
									<xsl:value-of select="apidata/@name" />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:for-each>
				</xsl:if>
			</xsl:when>
			<!-- all member lists get unqualified entries, qualified entries, and unqualified sub-entries -->
			<xsl:when test="$group='list' and $subgroup='members'">
				<xsl:variable name="typeSubgroup" select="/document/reference/apidata/@subgroup" />
				<xsl:variable name="names">
					<xsl:for-each select="/document/reference">
						<xsl:call-template name="textNames" />
					</xsl:for-each>
				</xsl:variable>
				<xsl:for-each select="msxsl:node-set($names)/name">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="{$typeSubgroup}IndexEntry">
							<parameter>
								<xsl:value-of select="." />
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="membersIndexEntry">
							<parameter>
								<include item="{$typeSubgroup}IndexEntry">
									<parameter>
										<xsl:value-of select="." />
									</parameter>
								</include>
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
				</xsl:for-each>
				<xsl:variable name="qnames">
					<xsl:for-each select="/document/reference">
						<xsl:call-template name="qualifiedTextNames" />
					</xsl:for-each>
				</xsl:variable>
				<xsl:for-each select="msxsl:node-set($qnames)/name">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="{$typeSubgroup}IndexEntry">
							<parameter>
								<xsl:value-of select="." />
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
				</xsl:for-each>
				<!-- enumeration topics also get entries for each member -->
			</xsl:when>
			<!-- other member list pages get unqualified sub-entries -->
			<xsl:when test="$group='list' and not($subgroup = 'overload')">
				<xsl:variable name="typeSubgroup" select="/document/reference/apidata/@subgroup" />
				<xsl:variable name="names">
					<xsl:for-each select="/document/reference">
						<xsl:call-template name="textNames" />
					</xsl:for-each>
				</xsl:variable>
				<xsl:for-each select="msxsl:node-set($names)/name">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="{$subgroup}IndexEntry">
							<parameter>
								<include item="{$typeSubgroup}IndexEntry">
									<parameter>
										<xsl:value-of select="." />
									</parameter>
								</include>
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
				</xsl:for-each>
			</xsl:when>
			<!-- constructor (or constructor overload) topics get unqualified sub-entries using the type names -->
			<xsl:when test="($subgroup='constructor' and not(/document/reference/memberdata/@overload)) or ($subgroup='overload' and /document/reference/apidata/@subgroup = 'constructor')">
				<xsl:variable name="typeSubgroup" select="/document/reference/containers/type/apidata/@subgroup" />
				<xsl:variable name="names">
					<xsl:for-each select="/document/reference/containers/type">
						<xsl:call-template name="textNames" />
					</xsl:for-each>
				</xsl:variable>
				<xsl:for-each select="msxsl:node-set($names)/name">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="constructorIndexEntry">
							<parameter>
								<include item="{$typeSubgroup}IndexEntry">
									<parameter>
										<xsl:value-of select="." />
									</parameter>
								</include>
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
				</xsl:for-each>
				<xsl:variable name="qnames">
					<xsl:for-each select="/document/reference">
						<xsl:call-template name="qualifiedTextNames" />
					</xsl:for-each>
				</xsl:variable>
				<xsl:for-each select="msxsl:node-set($qnames)/name">
					<MSHelp:Keyword Index="K">
						<includeAttribute name="Term" item="constructorTypeIndexEntry">
							<parameter>
								<xsl:value-of select="." />
							</parameter>
						</includeAttribute>
					</MSHelp:Keyword>
				</xsl:for-each>
			</xsl:when>
			<!-- other member (or overload) topics get qualified and unqualified entries using the member names -->
			<xsl:when test="($group='member' and not(/document/reference/memberdata/@overload)) or $subgroup='overload'">
				<!-- no index entries for explicit interface implementations -->
				<xsl:if test="not(/document/reference/proceduredata/@virtual='true' and /document/reference/memberdata/@visibility='private')">
					<xsl:variable name="entryType">
						<xsl:choose>
							<xsl:when test="string($subsubgroup)">
								<xsl:value-of select="$subsubgroup" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$subgroup='overload'">
										<xsl:value-of select="/document/reference/apidata/@subgroup"/>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="$subgroup" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:variable>
					<xsl:variable name="names">
						<xsl:for-each select="/document/reference">
							<xsl:call-template name="textNames" />
						</xsl:for-each>
					</xsl:variable>
					<xsl:for-each select="msxsl:node-set($names)/name">
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="{$entryType}IndexEntry">
								<parameter>
									<xsl:value-of select="." />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:for-each>
					<xsl:variable name="qnames">
						<xsl:for-each select="/document/reference">
							<xsl:call-template name="qualifiedTextNames" />
						</xsl:for-each>
					</xsl:variable>
					<xsl:for-each select="msxsl:node-set($qnames)/name">
						<MSHelp:Keyword Index="K">
							<includeAttribute name="Term" item="{$entryType}IndexEntry">
								<parameter>
									<xsl:value-of select="." />
								</parameter>
							</includeAttribute>
						</MSHelp:Keyword>
					</xsl:for-each>
				</xsl:if>
			</xsl:when>
			<!-- derived type lists get unqualified sub-entries -->
		</xsl:choose>
	</xsl:template>

	<xsl:template name="typeNameWithTicks">
		<xsl:for-each select="type|(containers/type)">
			<xsl:call-template name="typeNameWithTicks" />
			<xsl:text>.</xsl:text>
		</xsl:for-each>
		<xsl:value-of select="apidata/@name" />
		<xsl:if test="boolean(templates/template)">
			<xsl:text>`</xsl:text>
			<xsl:value-of select="count(templates/template)"/>
		</xsl:if>
	</xsl:template>

	<xsl:template name="qualifiedTextNames">
		<xsl:choose>
			<!-- explicit interface implementations -->
			<xsl:when test="memberdata[@visibility='private'] and proceduredata[@virtual = 'true']">
				<xsl:variable name="left">
					<xsl:for-each select="containers/type">
						<xsl:call-template name="textNames"/>
					</xsl:for-each>
				</xsl:variable>
				<xsl:variable name="right">
					<xsl:for-each select="implements/member">
						<xsl:call-template name="textNames" />
					</xsl:for-each>
				</xsl:variable>
				<xsl:call-template name="combineTextNames">
					<xsl:with-param name="left" select="msxsl:node-set($left)" />
					<xsl:with-param name="right" select="msxsl:node-set($right)" />
				</xsl:call-template>
			</xsl:when>
			<!-- members get qualified by type name -->
			<xsl:when test="apidata/@group='member' and containers/type">
				<xsl:variable name="left">
					<xsl:for-each select="containers/type">
						<xsl:call-template name="textNames"/>
					</xsl:for-each>
				</xsl:variable>
				<xsl:variable name="right">
					<xsl:call-template name="simpleTextNames" />
				</xsl:variable>
				<xsl:call-template name="combineTextNames">
					<xsl:with-param name="left" select="msxsl:node-set($left)" />
					<xsl:with-param name="right" select="msxsl:node-set($right)" />
				</xsl:call-template>
			</xsl:when>
			<!-- types get qualified by namespace name -->
			<xsl:when test="typedata and containers/namespace/apidata/@name">
				<xsl:variable name="left">
					<xsl:for-each select="containers/namespace">
						<xsl:call-template name="simpleTextNames"/>
					</xsl:for-each>
				</xsl:variable>
				<xsl:variable name="right">
					<xsl:call-template name="textNames" />
				</xsl:variable>
				<xsl:call-template name="combineTextNames">
					<xsl:with-param name="left" select="msxsl:node-set($left)" />
					<xsl:with-param name="right" select="msxsl:node-set($right)" />
				</xsl:call-template>
			</xsl:when>
		</xsl:choose>
	</xsl:template>

	<!-- given two XML lists of API names (produced by textNames template below), produces an XML list
  that dot-concatenates them, respecting the @language attributes -->
	<xsl:template name="combineTextNames">
		<xsl:param name="left" />
		<xsl:param name="right" />
		<xsl:param name="concatenateOperator" select="'.'" />

		<xsl:choose>
			<xsl:when test="count($left/name) &gt; 1">
				<xsl:choose>
					<xsl:when test="count($right/name) &gt; 1">
						<!-- both left and right are multi-language -->
						<xsl:for-each select="$left/name">
							<xsl:variable name="language" select="@language" />
							<name language="{$language}">
								<xsl:apply-templates select="." />
								<xsl:copy-of select="$concatenateOperator" />
								<xsl:apply-templates select="$right/name[@language=$language]" />
							</name>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
						<!-- left is multi-language, right is not -->
						<xsl:for-each select="$left/name">
							<xsl:variable name="language" select="@language" />
							<name language="{$language}">
								<xsl:apply-templates select="." />
								<xsl:if test="$right/name">
									<xsl:copy-of select="$concatenateOperator"/>
								</xsl:if>
								<xsl:value-of select="$right/name"/>
							</name>
						</xsl:for-each>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="count($right/name) &gt; 1">
						<!-- right is multi-language, left is not -->
						<xsl:for-each select="$right/name">
							<xsl:variable name="language" select="@language" />
							<name language="{.}">
								<xsl:value-of select="$left/name"/>
								<xsl:if test="$left/name">
									<xsl:copy-of select="$concatenateOperator"/>
								</xsl:if>
								<xsl:apply-templates select="." />
							</name>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
						<!-- neiter is multi-language -->
						<name>
							<xsl:value-of select="$left/name"/>
							<xsl:if test="$left/name and $right/name">
								<xsl:copy-of select="$concatenateOperator"/>
							</xsl:if>
							<xsl:value-of select="$right/name"/>
						</name>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<!-- produces an XML list of API names; context is parent of apidata element -->
	<!-- if there are no templates: <name>Blah</name> -->
	<!-- if there are templates: <name langauge="c">Blah<T></name><name language="v">Blah(Of T)</name> -->
	<xsl:template name="simpleTextNames">
		<xsl:choose>
			<xsl:when test="specialization">
				<xsl:apply-templates select="specialization" mode="index">
					<xsl:with-param name="name" select="apidata/@name" />
				</xsl:apply-templates>
			</xsl:when>
			<xsl:when test="templates">
				<xsl:apply-templates select="templates" mode="index">
					<xsl:with-param name="name" select="apidata/@name" />
				</xsl:apply-templates>
			</xsl:when>
			<xsl:otherwise>
				<name>
					<xsl:choose>
						<xsl:when test="apidata/@subgroup = 'constructor'">
							<xsl:value-of select="containers/type/apidata/@name"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="apidata/@name"/>
						</xsl:otherwise>
					</xsl:choose>
				</name>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="textNames">
		<xsl:choose>
			<xsl:when test="typedata and (containers/type | type)">
				<xsl:variable name="left">
					<xsl:apply-templates select="type | (containers/type)" mode="index" />
				</xsl:variable>
				<xsl:variable name="right">
					<xsl:call-template name="simpleTextNames" />
				</xsl:variable>
				<xsl:call-template name="combineTextNames">
					<xsl:with-param name="left" select="msxsl:node-set($left)" />
					<xsl:with-param name="right" select="msxsl:node-set($right)" />
				</xsl:call-template>
			</xsl:when>
			<xsl:when test="type">
				<xsl:variable name="left">
					<xsl:apply-templates select="type" mode="index" />
				</xsl:variable>
				<xsl:variable name="right">
					<xsl:call-template name="simpleTextNames" />
				</xsl:variable>
				<xsl:call-template name="combineTextNames">
					<xsl:with-param name="left" select="msxsl:node-set($left)" />
					<xsl:with-param name="right" select="msxsl:node-set($right)" />
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="simpleTextNames" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<!-- produces a C#/C++ style generic template parameter list for inclusion in the index -->
	<xsl:template name="csTemplateText">
		<xsl:text>%3C</xsl:text>
		<xsl:call-template name="templateText" />
		<xsl:text>%3E</xsl:text>
	</xsl:template>

	<!-- produces a VB-style generic template parameter list for inclusion in the index -->
	<xsl:template name="vbTemplateText">
		<xsl:text>(Of </xsl:text>
		<xsl:call-template name="templateText" />
		<xsl:text>)</xsl:text>
	</xsl:template>

	<!-- produces a comma-seperated list of generic template parameter names -->
	<!-- comma character is URL-encoded so as not to create sub-index entries -->
	<xsl:template name="templateText">
		<xsl:for-each select="*">
			<xsl:apply-templates select="." mode="index" />
			<xsl:if test="not(position()=last())">
				<xsl:text>%2C </xsl:text>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>


	<xsl:template match="specialization | templates" mode="index" >
		<xsl:param name="name" />
		<name language="c">
			<xsl:value-of select="$name" />
			<xsl:call-template name="csTemplateText" />
		</name>
		<name language="v">
			<xsl:value-of select="$name" />
			<xsl:call-template name="vbTemplateText" />
		</name>
	</xsl:template>

	<xsl:template match="template" mode="index">
		<xsl:value-of select="@name" />
	</xsl:template>

	<xsl:template match="arrayOf" mode="index">
		<name language="c">
			<xsl:apply-templates select="type|arrayOf|pointerTo|referenceTo|template|specialization|templates" mode="index"/>
			<xsl:text>[</xsl:text>
			<xsl:if test="number(@rank) &gt; 1">,</xsl:if>
			<xsl:text>]</xsl:text>
		</name>
		<name language="v">
			<xsl:apply-templates select="type|arrayOf|pointerTo|referenceTo|template|specialization|templates" mode="index"/>
			<xsl:text>(</xsl:text>
			<xsl:if test="number(@rank) &gt; 1">,</xsl:if>
			<xsl:text>)</xsl:text>
		</name>
	</xsl:template>

	<xsl:template match="pointerTo" mode="index">
		<xsl:apply-templates select="type|arrayOf|pointerTo|referenceTo|template|specialization|templates" mode="index"/>
		<xsl:text>*</xsl:text>
	</xsl:template>

	<xsl:template match="referenceTo" mode="index">
		<xsl:apply-templates select="type|arrayOf|pointerTo|referenceTo|template|specialization|templates" mode="index"/>
	</xsl:template>

	<xsl:template match="type" mode="index">
		<xsl:call-template name="textNames" />
	</xsl:template>

	<xsl:template match="name/name">
		<xsl:variable name="lang" select="ancestor::*/@language"/>

		<xsl:if test="not(@language) or @language = $lang">
			<xsl:value-of select="."/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="name/text()">
		<xsl:value-of select="."/>
	</xsl:template>

	<xsl:template name="operatorTextNames">
		<xsl:variable name="left">
			<xsl:if test="parameters/parameter[1]">
				<xsl:choose>
					<xsl:when test="parameters/parameter[1]//specialization | parameters/parameter[1]//templates | parameters/parameter[1]//arrayOf">
						<xsl:apply-templates select="parameters/parameter[1]" mode="index" />
					</xsl:when>
					<xsl:otherwise>
						<name>
							<xsl:apply-templates select="parameters/parameter[1]" mode="index" />
						</name>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>
		</xsl:variable>

		<xsl:variable name="right">
			<xsl:if test="returns[1]">
				<xsl:choose>
					<xsl:when test="returns[1]//specialization | returns[1]//templates | returns[1]//arrayOf">
						<xsl:apply-templates select="returns[1]" mode="index" />
					</xsl:when>
					<xsl:otherwise>
						<name>
							<xsl:apply-templates select="returns[1]" mode="index" />
						</name>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>
		</xsl:variable>

		<xsl:call-template name="combineTextNames">
			<xsl:with-param name="left" select="msxsl:node-set($left)" />
			<xsl:with-param name="right" select="msxsl:node-set($right)" />
			<xsl:with-param name="concatenateOperator">
				<xsl:text> to </xsl:text>
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:stylesheet>
