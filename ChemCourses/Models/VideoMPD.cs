
using System.Xml.Serialization;

namespace ChemCourses.Models
{
    [XmlRoot(ElementName = "S")]
    public class Segment
    {
        [XmlAttribute(AttributeName = "d")] public int Duration { get; set; }
        [XmlAttribute(AttributeName = "r")] public int RepeatAfter { get; set; }
    }

    [XmlRoot(ElementName = "SegmentTimeline")]
    public class SegmentTimeline
    {
        [XmlElement(ElementName = "S")] public List<Segment> Segments { get; set; }
    }

    [XmlRoot(ElementName = "SegmentTemplate")]
    public class SegmentTemplate
    {
        [XmlElement(ElementName = "SegmentTimeline")]
        public List<Segment> SegmentTimeline { get; set; }

        [XmlAttribute(AttributeName = "initialization")]
        public string? Initialization { get; set; }

        [XmlAttribute(AttributeName = "media")]
        public string? Media { get; set; }

        [XmlAttribute(AttributeName = "startNumber")]
        public int StartNumber { get; set; }

        [XmlAttribute(AttributeName = "timescale")]
        public int Timescale { get; set; }
    }

    [XmlRoot(ElementName = "Representation")]
    public class Representation
    {
        [XmlElement(ElementName = "AudioChannelConfiguration")]
        public AudioChannelConfiguration AudioChannelConfiguration { get; set; }

        [XmlAttribute(AttributeName = "bandwidth")]
        public int Bandwidth { get; set; }

        [XmlAttribute(AttributeName = "codecs")]
        public string Codecs { get; set; }

        [XmlAttribute(AttributeName = "frameRate")]
        public string FrameRate { get; set; }

        [XmlAttribute(AttributeName = "height")]
        public int Height { get; set; }

        [XmlAttribute(AttributeName = "id")] public string Id { get; set; }

        [XmlAttribute(AttributeName = "scanType")]
        public string ScanType { get; set; }

        [XmlAttribute(AttributeName = "width")]
        public int Width { get; set; }

        [XmlAttribute(AttributeName = "audioSamplingRate")]
        public int AudioSamplingRate { get; set; }
    }

    [XmlRoot(ElementName = "AudioChannelConfiguration")]
    public class AudioChannelConfiguration
    {
        [XmlAttribute(AttributeName = "schemeIdUri")]
        public string SchemeIdUri { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "AdaptationSet")]
    public class AdaptationSet
    {
        [XmlElement(ElementName = "SegmentTemplate")]
        public SegmentTemplate SegmentTemplate { get; set; }

        [XmlElement(ElementName = "Representation")]
        public Representation Representation { get; set; }

        [XmlAttribute(AttributeName = "maxHeight")]
        public string? MaxHeight { get; set; }

        [XmlAttribute(AttributeName = "maxWidth")]
        public string? MaxWidth { get; set; }

        [XmlAttribute(AttributeName = "mimeType")]
        public string? MimeType { get; set; }

        [XmlAttribute(AttributeName = "segmentAlignment")]
        public bool SegmentAlignment { get; set; }

        [XmlAttribute(AttributeName = "startWithSAP")]
        public int StartWithSAP { get; set; }
    }

    [XmlRoot(ElementName = "Period")]
    public class Period
    {
        [XmlElement(ElementName = "AdaptationSet")]
        public List<AdaptationSet> AdaptationSet { get; set; }
    }

    [XmlRoot(ElementName = "MPD")]
    public class VideoMPD
    {
        [XmlElement(ElementName = "Period")] public Period Period { get; set; }

        [XmlAttribute(AttributeName = "mediaPresentationDuration")]
        public string? MediaPresentationDuration { get; set; }

        [XmlAttribute(AttributeName = "minBufferTime")]
        public string? MinBufferTime { get; set; }

        [XmlAttribute(AttributeName = "profiles")]
        public string? Profiles { get; set; }

        [XmlAttribute(AttributeName = "type")] public string? Type { get; set; }
    }
}