module Paket.Domain

type NuGetSourceUrl =
    | NuGetSourceUrl of string

type EncryptedPassword =
    | EncryptedPassword of data: byte[] * salt: byte[]

type Credential =
    | Credential of username: string * password: EncryptedPassword

type ReferencesMode =
    | DirectAndInferred
    | OnlyDirect

type ContentFilesMode =
    | InstallContentFiles
    | OmitContentFiles

type EnvironmentVariable =
    | EnvironmentVariable of name: string

type NugetSourceAuthentication =
    | PlainTextAuthentication of username: string * password: string
    | EnvironmentVariableAuthentication of username: EnvironmentVariable * password: EnvironmentVariable
    | ConfigAuthentication

type NuGetSource =
    | Remote of url: NuGetSourceUrl * NugetSourceAuthentication
    | Local of path: string

type PackageSource =
    | NuGet of source: NuGetSource

// contents of paket.config, paket.dependencies, paket.lock
type Configuration =
    { Credentials: Map<NuGetSourceUrl, Credential>
      ReferencesMode: ReferencesMode option
      ContentFilesMode: ContentFilesMode option
      PackageSources: PackageSource list }



type PackageId =
    | PackageId of string

// TODO: specify components, see SemVer
type Version = Version

type VersionRangeBound =
    | Excluding
    | Including

type VersionRange =
    | Minimum of Version
    | GreaterThan of Version
    | Maximum of Version
    | LessThan of Version
    | Specific of Version
    | OverrideAll of Version
    | Range of fromBound: VersionRangeBound * fromVersion: Version * toVersion: Version * toBound: VersionRangeBound

type PreReleaseChannel =
    | PreReleaseChannel of string

type PreReleaseChannels =
    | None
    | All
    | Concrete of PreReleaseChannel list

type VersionRequirement =
    | VersionRequirement of VersionRange * PreReleaseChannels

// TODO: rename the options and type
type ResolverStrategy =
    | Max
    | Min

type PackageRequirement =
    | PackageRequirement of PackageId * VersionRequirement

type SourceFileId =
    | SourceFileId of string

type GitHubRepository =
    | GitHubRepository of owner: string * project: string * commit: string option

type SourceFileRequirement =
    | GitHub of SourceFileId * GitHubRepository
    | Gist of SourceFileId * GitHubRepository
    | Http of SourceFileId * url: string

// contents of paket.dependencies
type Requirements =
    { PackageRequirements: (PackageRequirement * ResolverStrategy) list
      SourceFileRequirements: SourceFileRequirement list }



type PackageReference =
    | PackageReference of PackageId

type SourceFileProjectLinkPath =
    | SourceFileProjectLinkPath of string

type SourceFileReference =
    | SourceFileReference of SourceFileId * SourceFileProjectLinkPath option

// contents of paket.references
type References =
    { Packages: PackageReference list
      SourceFiles: SourceFileReference list }


type Package =
    | Package of PackageId * Version * PackageRequirement list
type SourceFile =
    | SourceFile of SourceFileRequirement * PackageRequirement list

// contents of paket.lock
type Dependencies =
    { Packages: (PackageSource * Package) list
      SourceFiles: SourceFile list }
